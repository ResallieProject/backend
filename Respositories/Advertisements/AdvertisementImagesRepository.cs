using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel;
using Resallie.Data;
using Resallie.Models.Advertisements;

namespace Resallie.Respositories.Advertisements
{
    public class AdvertisementImagesRepository
    {
        //private static IWebHostEnvironment _environment;
        private readonly AppDbContext _ctx;

        public AdvertisementImagesRepository(AppDbContext ctx, IWebHostEnvironment environment)
        {
            _ctx = ctx;
            //_environment = environment;
        }


        public async void StoreImages(Advertisement advertisement, IFormFileCollection objFiles)
        {
            foreach (var file in objFiles)
            {
                //Validate(file);
                try
                {
                    Run(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task Run(IFormFile file)
        {
            var args = AdImagesStoreApi.BasePutObjectArgs;
            var client = AdImagesStoreApi.AdImagesStoreClient;
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            using var filestream = new MemoryStream(fileBytes);
            args.WithObject("Images/" + file.FileName)
            .WithStreamData(filestream)
            .WithObjectSize(filestream.Length)
            .WithContentType("application/octet-stream");
            await client.PutObjectAsync(args);

            StatObjectArgs stat = AdImagesStoreApi.BaseStatObjectArgs
                    .WithObject("Images/" + file.FileName);

            ObjectStat objectArgs = await client.StatObjectAsync(stat);
        }
    }
}