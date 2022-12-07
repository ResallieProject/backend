using Resallie.Models;
using Resallie.Respositories.Advertisements;

namespace Resallie.Services.Advertisements;

public class AdvertisementService
{
    private readonly AdvertisementRepository _repository;
    private readonly AdvertisementFeatureRepository _afRepository;

    public AdvertisementService(AdvertisementRepository repository, AdvertisementFeatureRepository afRepository)
    {
        _repository = repository;
        _afRepository = afRepository;
        
    }

    public async Task<Advertisement> Create(Advertisement advertisement)
    {
        
        await _repository.Create(advertisement);

        return advertisement;
    }

    public async Task<bool> Delete(int id)
    {
        await _afRepository.DeleteMany(id);
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
    
    public async Task<Advertisement> Update(Advertisement advertisement, Advertisement oldAdvertisement)
    {
        oldAdvertisement.Title = advertisement.Title;
        oldAdvertisement.Description = advertisement.Description;
        oldAdvertisement.Defects = advertisement.Defects;
        oldAdvertisement.IsExpired = advertisement.IsExpired;
        oldAdvertisement.Price = advertisement.Price;
        oldAdvertisement.CategoryId = advertisement.CategoryId;

        await _afRepository.DeleteMany(oldAdvertisement.Id);

        oldAdvertisement.Features = advertisement.Features;
        
        await _repository.Update(oldAdvertisement);
        
        return advertisement;
    }
}