using Resallie.Data;
using Resallie.Models.Advertisements;
using Resallie.Respositories.Advertisements;

namespace Resallie.Services.Advertisements;

public class AdvertisementService
{
    private readonly AdvertisementRepository _repository;
    private readonly AdvertisementFeatureRepository _afRepository;
    private readonly AdvertisementImagesRepository _imgRepository;

    public AdvertisementService(
        AdvertisementRepository repository,
        AdvertisementFeatureRepository afRepository,
        AdvertisementImagesRepository imgRepository
        )
    {
        _repository = repository;
        _afRepository = afRepository;
        _imgRepository = imgRepository;
    }

    public async Task<Advertisement> Create(Advertisement advertisement, IFormFileCollection? collection)
    {
        if (collection.Count > 0)
        {
            foreach (var item in collection)
            {
                if (!Validate(item))
                {
                    throw new InvalidDataException("Filesize is greater than 7 mb of isn't from the right extension .png or .jpg");
                }
            }
        }

        await _repository.Create(advertisement);

        if (collection != null)
        {
            //AppDbContext temp = new AppDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext>());
            //new Task(() => _imgRepository.StoreImages(advertisement, collection, temp)
            //).Start();
            await _imgRepository.StoreImages(advertisement, collection);
        }

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
        _imgRepository.DeleteMany(oldAdvertisement, advertisement);

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

    private static bool Validate(IFormFile file)
    {
        long allowedSize = 7;
        if (file.ContentType != "image/png"
            && file.ContentType != "image/jpg"
            && file.ContentType != "image/jpeg"
            || file.Length > allowedSize * 1048576)
        {
            return false;
        }

        return true;
    }
}