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
            //Acces Key ID: SCWPMTJRRN78RY602TM4 
            //Secret Key: 0085c57c-6b76-4ea6-849d-510ea7b9125e
            //Project ID: 5be1c470-325a-40da-8187-cdcbbe62b418
            try
            {
                string strinUrl = String.Format("");
            }
            catch
            {

            }
        }
    }
}
