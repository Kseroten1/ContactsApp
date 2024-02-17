using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ContactContext _ctx;
    public CategoryRepository(ContactContext ctx)
    {
        _ctx = ctx;
    }
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        var categories = await _ctx.Categories.ToListAsync();
        return categories;
    }
    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _ctx.Categories.Include(s => s.SubCategories).SingleOrDefaultAsync(c => c.CategoryId == id);
    }
    public async Task UpdateCategory(Category category)
    {
        _ctx.Categories.Update(category);
        await _ctx.SaveChangesAsync();
    }
    public async Task AddSubCategory(SubCategory subCategory)
    {
        _ctx.SubCategories.Add(subCategory);
        await _ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<SubCategory>> GetSubCategories()
    {
        var subCategories = await _ctx.SubCategories.ToListAsync();
        return subCategories;
    }
}

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetByIdAsync(int id);
    Task AddSubCategory(SubCategory subCategory);
    Task<IEnumerable<SubCategory>> GetSubCategories();
}