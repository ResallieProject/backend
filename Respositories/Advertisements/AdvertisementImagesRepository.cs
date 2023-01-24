using Microsoft.AspNetCore.Mvc;
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

        public void StoreImages(Advertisement advertisement, IFormFileCollection objFiles)
        {
            int order = 1;
            foreach (var file in objFiles)
            {
                if (Validate(file))
                {
                    _ = Run(file, advertisement.UserId, advertisement.Id, order);
                    order++;
                }
                else
                {
                    throw new InvalidDataException("Filesize is greater than 7 mb of isn't from the right extension .png or .jpg");
                }
            }
           
            _ctx.SaveChangesAsync();
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

            Random random = new();
            string destignation = $"Images/{UserId}/" + string.GetHashCode(file.FileName) + random.Next((int)file.Length / 100);
            using var filestream = new MemoryStream(fileBytes);
            args.WithObject(destignation)
            .WithStreamData(filestream)
            .WithObjectSize(filestream.Length)
            .WithContentType("application/octet-stream");
            await client.PutObjectAsync(args);

            if (CheckSuccesFullyStoredAsync(destignation, client).Result)
            {
                _ctx.AdvertisementImages.Add(new AdvertisementImage() { AdvertisementId = AdvertisementId, Path = destignation, Order = order });
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
    