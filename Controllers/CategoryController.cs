using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resallie.Models;
using Resallie.Services.Categories;

namespace Resallie.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService _service;

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