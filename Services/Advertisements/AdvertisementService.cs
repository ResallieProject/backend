using Resallie.Models;
using Resallie.Models.Advertisements;
using Resallie.Respositories.Advertisements;

namespace Resallie.Services.Advertisements;

public class AdvertisementService
{
    private AdvertisementRepository _repository;
    private AdvertisementFeatureRepository _afRepository;
    private AdvertisementImagesRepository _imgRepository;

    public AdvertisementService(
        AdvertisementRepository repository,
        AdvertisementFeatureRepository afRepository, AdvertisementImagesRepository imgRepository)
    {
        _repository = repository;
        _afRepository = afRepository;
        _imgRepository= imgRepository;
    }

    public async Task<Advertisement> Create(Advertisement advertisement, IFormFileCollection? collection)
    {
        if (collection != null) 
        {
            //await _imgRepository.StoreImages(advertisement, collection);
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

    public async Task<List<Advertisement>> GetAll(string? searchParams)
    {
        return await _repository.GetAll(searchParams);
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