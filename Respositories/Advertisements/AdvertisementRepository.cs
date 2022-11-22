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

    public async Task<bool> Delete(int id)
    {
        var advertisement = await _ctx.Advertisements.FindAsync(id);
        if (advertisement == null)
        {
            return false;
        }

        _ctx.Advertisements.Remove(advertisement);
        await _ctx.SaveChangesAsync();

        return true;
    }

    public async Task<Advertisement?> Get(int id)
    {
        var advertisement = await _ctx.Advertisements.FindAsync(id);
        if (advertisement == null)
        {
            return null;
        }

        await _ctx.Entry(advertisement).Reference(ad => ad.Category).LoadAsync();

        return advertisement;
    }

    public async Task<List<Advertisement>> GetAll()
    {
        var advertisements = await _ctx.Advertisements.ToListAsync();
        foreach (var advertisement in advertisements)
        {
            await _ctx.Entry(advertisement).Reference(ad => ad.Category).LoadAsync();
        }

        return advertisements;
    }
}