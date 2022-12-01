using Microsoft.EntityFrameworkCore;
using Resallie.Data;
using Resallie.Models;

namespace Resallie.Respositories.Advertisements;

public class AdvertisementFeatureRepository
{
    private AppDbContext _ctx;

    public AdvertisementFeatureRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<AdvertisementFeature>> DeleteMany(int advertisementId)
    {
        var features = await  _ctx.AdvertisementFeatures.Where(af => af.AdvertisementId == advertisementId).ToListAsync();
        
        _ctx.AdvertisementFeatures.RemoveRange(features);
        await _ctx.SaveChangesAsync();

        return features;
    }
}