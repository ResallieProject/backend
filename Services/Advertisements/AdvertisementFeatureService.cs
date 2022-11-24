using Resallie.Models;
using Resallie.Respositories.Advertisements;

namespace Resallie.Services.Advertisements;

public class AdvertisementFeatureService
{
    private AdvertisementFeatureRepository _repository;

    public AdvertisementFeatureService(AdvertisementFeatureRepository respository)
    {
        _repository = respository;
    }

    public async Task<List<AdvertisementFeature>> Create(List<AdvertisementFeature> features)
    {
        return await _repository.Insert(features);
    }
}