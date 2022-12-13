using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resallie.Models;
using Resallie.Requests.Authentication;
using Resallie.Services.Authentication;

namespace Resallie.Controllers.Authentication;

[Route("[controller]")]
[ApiController]
public class AuthenticationController : BaseController
{
    private readonly AuthenticationService _service;

    public AuthenticationController(AuthenticationService service)
    {
        _service = service;
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] User request)
    {
        var user = await _service.RegisterUser(request);
        
        if (user == null)
            return BadRequest(new { message = "Email is already taken" });

        return Ok(user);
    }
    
    [HttpPost("Authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
    {
        var user = await _service.AuthenticateUser(request.Email, request.Password);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(user);
    }
}