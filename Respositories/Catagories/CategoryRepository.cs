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
    }
}