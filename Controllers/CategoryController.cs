using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resallie.Models;
using Resallie.Services.Categories;

namespace Resallie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<Category> Index()
        {
            return _service.GetAllCategories();
        }
    }
}