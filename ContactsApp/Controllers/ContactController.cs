using ContactsApp.DTO;
using ContactsApp.Models;
using ContactsApp.Repositories;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Cors;

namespace ContactsApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactsRepository _contactsRepository;
    private readonly ICategoryRepository _categoryRepository;
    public ContactController(IContactsRepository contactsRepository, ICategoryRepository categoryRepository)
    {
        _contactsRepository = contactsRepository;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    [Route("Subcategory")]
    public async Task<IEnumerable<SubCategory>> GetSubCategories()
    {
        var subCategories = await _categoryRepository.GetSubCategories();
        return subCategories;
    }

    [HttpGet]
    [Route("Category")]
    public async Task<IEnumerable<Category>> GetCategories()
    {
        var categories = await _categoryRepository.GetCategoriesAsync();
        return categories;
    }

    [HttpGet]
    public async Task<IEnumerable<GetAllContactsResult>> Get()
    {
        var contacts = await _contactsRepository.GetAllAsync();
        return contacts.Select(c => new GetAllContactsResult(c.ContactId, c.FirstName, c.LastName, c.Category.Name));
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<GetContact>> GetByID(int id)
    {
        var contact = await _contactsRepository.GetByIdAsync(id);
        if (contact is null)
        {
            return NotFound();
        }
        return new GetContact(contact.ContactId, contact.FirstName, contact.LastName, contact.PhoneNumber, contact.Email, contact.Category.Name, contact.SubCategory?.Name, contact.DateOfBirth);

    }
    [HttpDelete]
    [Route("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteById(int id)
    {
        var contact = await _contactsRepository.GetByIdAsync(id);
        if (contact is not null)
        {
            await _contactsRepository.DeleteAsync(contact);
            return Ok();
        }
        return NotFound();
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Add([FromBody] AddContact newContact)
    {
        var category = await _categoryRepository.GetByIdAsync(newContact.CategoryId);
        if (category is null)
        {
            return BadRequest();
        }
        var subCategory = category.SubCategories.SingleOrDefault(c => c.Name == newContact.SubCategoryName);
        if (!category.AllowCustomSubCategory)
        {
            if (subCategory is null)
            {
                return BadRequest();
            }
        }
        else
        {
            if (subCategory is null)
            {
                if (newContact.SubCategoryName is not null)
                {
                    subCategory = new SubCategory { Name = newContact.SubCategoryName };
                    category.SubCategories.Add(subCategory);
                }
                else
                    return BadRequest();
            }
        }

        if (await _contactsRepository.GetByEmailAsync(newContact.Email) is not null)
        {
            return BadRequest();
        }

        var contact = new Contact()
        {
            FirstName = newContact.FirstName,
            LastName = newContact.LastName,
            PhoneNumber = newContact.PhoneNumber,
            Email = newContact.Email,
            CategoryId = newContact.CategoryId,
            SubCategory = subCategory,
            DateOfBirth = newContact.BirthDate,
        };
        await _contactsRepository.AddAsync(contact);
        return Created();
    }
    [HttpPut]
    [Authorize]
    public async Task<ActionResult> Update([FromBody] UpdateContact updateContact)
    {
        var contact = await _contactsRepository.GetByIdAsync(updateContact.contactId);
        if (contact is null) return BadRequest();
        contact.FirstName = updateContact.FirstName;
        contact.LastName = updateContact.LastName;
        contact.PhoneNumber = updateContact.PhoneNumber;
        if (await _contactsRepository.GetByEmailAsync(updateContact.Email) is not null) return BadRequest();
        contact.Email = updateContact.Email;
        var category = await _categoryRepository.GetByIdAsync(updateContact.CategoryId);
        if (category is null) return BadRequest();
        contact.CategoryId = updateContact.CategoryId;
        var subCategory = category.SubCategories.SingleOrDefault(c => c.Name == updateContact.SubCategoryName);
        if (!category.AllowCustomSubCategory)
        {
            if (subCategory is null)
            {
                return BadRequest();
            }
        }
        else
        {
            if (subCategory is null)
            {
                if (updateContact.SubCategoryName is not null)
                {
                    subCategory = new SubCategory { Name = updateContact.SubCategoryName };
                    category.SubCategories.Add(subCategory);
                }
                else
                    return BadRequest();
            }
        }
        contact.SubCategory = subCategory;
        contact.DateOfBirth = updateContact.BirthDate;
        await _contactsRepository.UpdateAsync(contact);
        return Ok();

    }
}
