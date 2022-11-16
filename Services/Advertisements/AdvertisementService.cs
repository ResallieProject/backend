using Resallie.Models;
using Resallie.Respositories.Advertisements;

namespace Resallie.Services.Advertisements;

public class AdvertisementService
{
    private AdvertisementRepository _repository;

    public AdvertisementService(AdvertisementRepository respository)
    {
        _repository = respository;
    }

    public async Task<Advertisement> Create(Advertisement advertisement)
    {
        await _repository.Create(advertisement);

        return advertisement;
    }
}