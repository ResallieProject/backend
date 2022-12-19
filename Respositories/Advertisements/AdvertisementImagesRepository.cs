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

        public void Upload()
        {

        }

    }
}
