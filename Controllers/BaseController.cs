using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Resallie.Controllers;

public class BaseController : ControllerBase
{
    [NonAction]
    public int GetCurrentUserId()
    {
        return 1;// int.Parse(HttpContext.User.FindFirstValue("UserId"));
    }
}