using Microsoft.AspNetCore.Mvc;
using Resallie.Models;
using Resallie.Services.Advertisements;

namespace Resallie.Controllers;

[Route("[controller]")]
[ApiController]
public class AdvertisementController : ControllerBase
{
    private AdvertisementService _service;

    public AdvertisementController(AdvertisementService service)
    {
        _service = service;
    }

    [HttpGet]
    public string Index()
    {
        return "Hi";
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Advertisement advertisement)
    {
        if (advertisement.Category != null) return BadRequest();

        advertisement = await _service.Create(advertisement);

        return Ok(advertisement);
    }
}