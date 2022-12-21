using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resallie.Data;
using Resallie.Models;
using Resallie.Models.Advertisements;

namespace Resallie.Respositories.Advertisements
{
    public class AdvertisementImagesRepository
    {
        private static IWebHostEnvironment _environment;
        private AppDbContext _ctx;

        public AdvertisementImagesRepository(AppDbContext ctx, IWebHostEnvironment environment)
        {
            _ctx = ctx;
            _environment = environment;
        }

        public async Task StoreImages(Advertisement advertisement, List<IFormFile> objFiles)
        {
            try
            {
                var path = Directory.CreateDirectory(_environment.WebRootPath + '/' + advertisement.Id + ".Uploads");

                using (FileStream fileStream = System.IO.File.Create(path.FullName))
                {

                }
            }

            catch
            {

            }
        }
    }
}
