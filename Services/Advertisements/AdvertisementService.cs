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
    
    public async Task<bool> Delete(int id)
    {
        return await _repository.Delete(id);
    }
    
    public async Task<Advertisement?> Get(int id)
    {
        return await _repository.Get(id);
    }

    public async Task<List<Advertisement>> GetAll()
    {
        return await _repository.GetAll();
    }
}