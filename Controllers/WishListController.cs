using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resallie.Controllers.Interfaces;
using Resallie.Models;
using Resallie.Services;
using Resallie.Services.Categories;

namespace Resallie.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WishListController : Controller
    {
        private IBaseService<UserWishList> _service;

        public WishListController (UserWishListService service)
        {
            _service = service;
        }
    }
}
