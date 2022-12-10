using Microsoft.AspNetCore.Mvc;
using Resallie.Models;
using Resallie.Services.Authentication;

namespace Resallie.Controllers.Authentication;

[Route("[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _service;

    public AuthenticationController(AuthenticationService service)
    {
        _service = service;
    }

    [HttpPost]
    async Task<IActionResult> Register([FromBody] User request)
    {
        var user = await _service.RegisterUser(request);

        return Ok(user);
    }
}