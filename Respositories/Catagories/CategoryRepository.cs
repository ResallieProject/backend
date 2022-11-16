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

        public List<Category> GetAllCategories()
        {
            return _ctx.Categories.ToList();
        }
    }
}