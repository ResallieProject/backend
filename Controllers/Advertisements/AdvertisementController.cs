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
    public Task<List<Advertisement>> Index()
    {
        return _service.GetAll();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Advertisement advertisement)
    {
        if (advertisement.Category != null) return BadRequest();

        advertisement = await _service.Create(advertisement);

        return Ok(advertisement);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Advertisement? advertisement = await _service.Get(id);
        
        if (advertisement == null) return NotFound();

        return Ok(advertisement);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        bool success = await _service.Delete(id);

        if (!success) return NotFound();

        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Advertisement advertisement)
    {
        if (advertisement.Category != null) return BadRequest();
        
        Advertisement? oldAdvertisement = await _service.Get(id);
        if (oldAdvertisement == null) return NotFound();
        
        advertisement = await _service.Update(advertisement, oldAdvertisement);


        return Ok(advertisement);
    }
}