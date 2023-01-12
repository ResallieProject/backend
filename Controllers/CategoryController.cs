using Microsoft.AspNetCore.Mvc;
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
    }
}