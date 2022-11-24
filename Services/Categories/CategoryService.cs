using Resallie.Models;
using Resallie.Respositories.Categories;

namespace Resallie.Services.Categories
{
    public class CategoryService
    {
        private CategoryRepository _repository;

        public CategoryService(CategoryRepository repository)
        {
            _repository = repository;
        }

        public List<Category> GetAllCategories()
        {
            return _repository.GetAllCategories();
        }
    }
}
