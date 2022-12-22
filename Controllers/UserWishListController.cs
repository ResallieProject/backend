using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resallie.Controllers.Interfaces;
using Resallie.Models;
using Resallie.Services;

namespace Resallie.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserWishListController : Controller
    {
        private readonly IBaseService<UserWishList> _service;

        public UserWishListController (UserWishListService service)
        {
            _service = (IBaseService<UserWishList>)service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return Ok(
                await _service.GetAllFromThisUser();
        }

    }
}
