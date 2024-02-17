using ContactsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Repositories;

public class ContactsRepository : IContactsRepository
{
    private readonly ContactContext _ctx;
    public ContactsRepository(ContactContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        var contacts = await _ctx.Contacts.Include(c => c.Category).ToListAsync();
        return contacts;
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _ctx.Contacts.Include(c => c.Category).Include(c => c.SubCategory).SingleOrDefaultAsync(c => c.ContactId == id);
    }
    public async Task<Contact?> GetByEmailAsync(string email)
    {
        return await _ctx.Contacts.Include(c => c.Category).Include(c => c.SubCategory).SingleOrDefaultAsync(c => c.Email == email);
    }

    public async Task DeleteAsync(Contact contact)
    {
        _ctx.Contacts.Remove(contact);
        await _ctx.SaveChangesAsync();
    }

    public async Task AddAsync(Contact contact)
    {
        _ctx.Contacts.Add(contact);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contact contact)
    {
        _ctx.Contacts.Update(contact);
        await _ctx.SaveChangesAsync();
    }
}

public interface IContactsRepository
{
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact?> GetByIdAsync(int id);
    Task DeleteAsync(Contact contact);
    Task AddAsync(Contact contact);
    Task<Contact?> GetByEmailAsync(string email);
    Task UpdateAsync(Contact contact);
}