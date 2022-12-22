using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resallie.Controllers.Interfaces;
using Resallie.Models;
using Resallie.Services;

namespace Resallie.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserWishListController : BaseController
    {
        private readonly UserWishListService _service;

        public UserWishListController (IBaseService<UserWishList> service)
        {
            _service = (UserWishListService)service;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            var id = GetCurrentUserId();
            return null;
            return Ok(
                await _service.GetAllFromThisUser(GetCurrentUserId()));
        }
    }
}
