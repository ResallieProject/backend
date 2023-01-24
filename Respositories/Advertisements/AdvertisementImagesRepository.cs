using System.IO;
using Minio;
using Minio.DataModel;
using Resallie.Data;
using Resallie.Models;
using Resallie.Models.Advertisements;

namespace Resallie.Respositories.Advertisements
{
    public class AdvertisementImagesRepository
    {
        readonly AppDbContext _ctx;
        static readonly PutObjectArgs args = AdImagesStoreApi.BasePutObjectArgs;
        static readonly MinioClient client = AdImagesStoreApi.AdImagesStoreClient;

        public AdvertisementImagesRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task StoreImages(Advertisement advertisement)
        {
            int order = 1;
            foreach (var image in advertisement.TempCollectedImages)
            {
                string destination = $"Images/{advertisement.UserId}/" + GenerateFileName(16) + Path.GetExtension(image.FileName).ToLower();

                await TransferToStorage(image, destination);
                await CheckSuccesFullyStoredAsync(destination);

                AdvertisementImage AdImg = new()
                {
                    AdvertisementId = advertisement.Id,
                    Order = order,
                    CreatedAt = DateTime.Now,
                    Path = destination
                };
                _ctx.AdvertisementImages.Add(AdImg);
            }
            _ctx.SaveChanges(); 
        }

        internal void DeleteMany(Advertisement oldAdvertisement, Advertisement advertisement)
        {
            foreach (var imageref in oldAdvertisement.Images)
            {
                if (advertisement.Images.Any(img => img.Id != imageref.Id))
                {
                    _ctx.AdvertisementImages.Remove(imageref);
                }
            }

            _ctx.SaveChangesAsync();
        }

        private static async Task TransferToStorage(IFormFile image, string destination)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            using var filestream = new MemoryStream(fileBytes);
            args.WithObject(destination)
            .WithStreamData(filestream)
            .WithObjectSize(filestream.Length)
            .WithContentType(image.ContentType);
            await client.PutObjectAsync(args);
        }

        public static string GenerateFileName(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[(new Random()).Next(s.Length)]).ToArray());
        }
        private static async Task CheckSuccesFullyStoredAsync(string destignation)
        {
            try
            {
                StatObjectArgs stat = AdImagesStoreApi.BaseStatObjectArgs.WithObject(destignation);

                ObjectStat objectArgs = await client.StatObjectAsync(stat);
            }

            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }
    }
}
