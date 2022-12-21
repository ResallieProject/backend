using Resallie.Models.Advertisements;
using Resallie.Respositories.Advertisements;

namespace Resallie.Services.Advertisements;

public class AdvertisementService

{
    private AdvertisementRepository _repository;
    private AdvertisementFeatureRepository _afRepository;
    private AdvertisementImagesRepository _aiRepository;

    public AdvertisementService(
        AdvertisementRepository repository,
        AdvertisementFeatureRepository afRepository, AdvertisementImagesRepository aiRepository
        )
    {
        _repository = repository;
        _afRepository = afRepository;
        _aiRepository = aiRepository;
    }

    public async Task<Advertisement> Create(Advertisement advertisement)
    {
        if(advertisement.Images.Count > 0)
        {
            await _aiRepository.StoreImages(advertisement, );
        }
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

        oldAdvertisement.Images = advertisement.Images;
        
        advertisement = await _repository.Update(oldAdvertisement);
        
        return advertisement;
    }
    
    public async Task<bool> IsAdvertisementOwner(int userId, int advertisementId)
    {
        Advertisement? advertisement = await _repository.Get(advertisementId);
        
        if (advertisement == null)
        {
            return false;
        }
        
        return advertisement.UserId == userId;
    }
}