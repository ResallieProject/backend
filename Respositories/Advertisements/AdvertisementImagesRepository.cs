using System.IO;
using Minio;
using Minio.DataModel;
using Resallie.Data;
using Resallie.Models.Advertisements;

namespace Resallie.Respositories.Advertisements
{
    public class AdvertisementImagesRepository
    {
        private readonly AppDbContext _ctx;

        public AdvertisementImagesRepository(AppDbContext ctx )
        {
            _ctx = ctx;
        }

        public async void StoreImages(Advertisement advertisement)
        {
            int order = 1;
            Task store;
            foreach (var file in advertisement.StoreImages)
            {
                if (Validate(file))
                {
                    store = Run(file, advertisement.UserId, advertisement.Id, order);
                    order++;
                }
                else
                {
                    throw new InvalidDataException("Filesize is greater than 7 mb of isn't from the right extension .png or .jpg");
                }
            }
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

        public async Task Run(IFormFile file, int UserId, int AdvertisementId, int order)
        {
            var args = AdImagesStoreApi.BasePutObjectArgs;
            var client = AdImagesStoreApi.AdImagesStoreClient;
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            string destination = "Images/" + GenerateFileName(16) + Path.GetExtension(file.FileName).ToLower();
            Console.WriteLine(destination);

            using var filestream = new MemoryStream(fileBytes);
            args.WithObject(destination)
            .WithStreamData(filestream)
            .WithObjectSize(filestream.Length)
            .WithContentType(file.ContentType);
            await client.PutObjectAsync(args);

            if (CheckSuccesFullyStoredAsync(destination, client).Result)
            {
                _ctx.AdvertisementImages.Add(new AdvertisementImage() { AdvertisementId = AdvertisementId, Path = destination, Order = order });
            }
        }

        private static async Task<bool> CheckSuccesFullyStoredAsync(string destignation, MinioClient client)
        {
            //To Check if the images was actually stored, this call will try to find the object
            try
            {
                StatObjectArgs stat = AdImagesStoreApi.BaseStatObjectArgs
                        .WithObject(destignation);

                ObjectStat objectArgs = await client.StatObjectAsync(stat);

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        
        public static string GenerateFileName(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[(new Random()).Next(s.Length)]).ToArray());
        }

        internal void DeleteMany(Advertisement oldAdvertisement, Advertisement advertisement)
        {
            foreach(var imageref in oldAdvertisement.Images)
            {
                if( advertisement.Images.Any(img => img.Id != imageref.Id))
                {
                    _ctx.AdvertisementImages.Remove(imageref);
                }
            }
            
            _ctx.SaveChangesAsync();
        }
    }
}
    