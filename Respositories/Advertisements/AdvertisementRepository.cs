using Microsoft.EntityFrameworkCore;
using Resallie.Data;
using Resallie.Models;

namespace Resallie.Respositories.Advertisements;

public class AdvertisementRepository
{
    private AppDbContext _ctx;

    public AdvertisementRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Advertisement> Create(Advertisement advertisement)
    {
        await _ctx.Advertisements.AddAsync(advertisement);
        await _ctx.SaveChangesAsync();

        await _ctx.Entry(advertisement).Reference(ad => ad.Category).LoadAsync();
        
        return advertisement;
    }
}