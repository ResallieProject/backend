using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Resallie.Controllers;

public class BaseController : ControllerBase
{
    public int GetCurrentUserId()
    {
        return int.Parse(HttpContext.User.FindFirstValue("UserId"));
    }
}