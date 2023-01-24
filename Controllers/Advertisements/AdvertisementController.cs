using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resallie.Models.Advertisements;
using Resallie.Services.Advertisements;

namespace Resallie.Controllers.Advertisements;

[Route("[controller]")]
[ApiController]
public class AdvertisementController : BaseController
{
    private readonly AdvertisementService _service;

    public AdvertisementController(AdvertisementService service)
    {
        _service = service;

    }

    [HttpGet]
    public Task<List<Advertisement>> Index([FromQuery(Name = "q")] string? searchParams)
    {
        return _service.GetAll(searchParams);
    }

    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Advertisement advertisement)
    {
        //Advertisement advertisement = new Advertisement();
        //advertisement.UserId = 5;
        //advertisement.Title = "title";
        //advertisement.Defects = "fdsa";
        //advertisement.CategoryId = 1;
        //advertisement.Description = "description"; 
        //advertisement.CreatedAt = DateTime.Now;
        //advertisement.UpdatedAt = DateTime.Now;
        //advertisement.Price = 0;
        //advertisement.Id = 1;

        if (advertisement.Category != null) return BadRequest();

        //advertisement.UserId = GetCurrentUserId();

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
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        bool isOwner = await _service.IsAdvertisementOwner(GetCurrentUserId(), id);
        if (!isOwner) return Forbid();
        
        bool success = await _service.Delete(id);

        if (!success) return BadRequest();

        return Ok();
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Advertisement advertisement)
    {
        if (advertisement.Category != null) return BadRequest();
        
        bool isOwner = await _service.IsAdvertisementOwner(GetCurrentUserId(), id);
        if (!isOwner) return Forbid();
        
        Advertisement? oldAdvertisement = await _service.Get(id);
        if (oldAdvertisement == null) return NotFound();
        
        advertisement = await _service.Update(advertisement, oldAdvertisement);

        return Ok(advertisement);
    }
}