using Microsoft.EntityFrameworkCore;
using Resallie.Data;
using Resallie.Models;

namespace Resallie.Respositories.Advertisements
{
    public class AdvertisementImagesRepository
    {
        private AppDbContext _ctx;

        public AdvertisementImagesRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Advertisement> Create(Advertisement advertisement)
        {
            await _ctx.Advertisements.AddAsync(advertisement);
            await _ctx.SaveChangesAsync();

            return await Get(advertisement.Id);
        }

        public async Task<Advertisement?> Get(int id)
        {
            var advertisement = await _ctx.Advertisements.FindAsync(id);
            if (advertisement == null)
            {
                return null;
            }

            await _ctx.Entry(advertisement).Reference(ad => ad.Category).LoadAsync();
            await _ctx.Entry(advertisement).Collection(ad => ad.).LoadAsync();

            return advertisement;
        }
    }
}
