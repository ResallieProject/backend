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

    public async Task<List<AdvertisementFeature>> Insert(List<AdvertisementFeature> features)
    {
        foreach(AdvertisementFeature feature in features)
        {
            _ctx.AdvertisementFeatures.Add(feature);
        }
        
        await _ctx.SaveChangesAsync();
        
        return features;
    }
}