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

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] User request)
    {
        var user = await _service.RegisterUser(request);
        
        if (user == null)
            return BadRequest(new { message = "Email is already taken" });

        return Ok(user);
    }
}