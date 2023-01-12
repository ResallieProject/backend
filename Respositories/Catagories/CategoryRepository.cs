using Microsoft.EntityFrameworkCore;
using Resallie.Data;
using Resallie.Models;

namespace Resallie.Respositories.Categories
{
    public class CategoryRepository
    {
        private AppDbContext _ctx;

        public CategoryRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories =  await _ctx.Categories
                .Where(c => c.CategoryId == null)
                .Include(c => c.Children
                    .OrderBy(child => child.Id)
                )
                .ToListAsync();
            
            return categories;
        }
        
        public async Task<Category?> GetCategoryById(int id)
        {
            var category = await _ctx.Categories
                .Include(c => c.Advertisements)
                .FirstOrDefaultAsync(c => c.Id == id);
            
            foreach (Advertisement advertisement in category.Advertisements)
            {
                advertisement.Category = null;
            }

            return category;
        }
    }
}