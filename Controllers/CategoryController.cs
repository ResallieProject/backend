using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resallie.Models;
using Resallie.Services.Categories;

namespace Resallie.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(
                await _service.GetAllCategories()
            );
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            Category? category = await _service.GetCategoryById(id);
            return category != null ? Ok(category) : NotFound();
        }
    }
}