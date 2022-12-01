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
            return await _ctx.Categories.ToListAsync();
        }
    }
}