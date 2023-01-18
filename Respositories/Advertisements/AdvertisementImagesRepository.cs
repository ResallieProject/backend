using System.Net;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
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

        [Obsolete]
        public async void StoreImages(Advertisement advertisement, IFormFileCollection objFiles)
        {
            //Acces Key ID: SCWPMTJRRN78RY602TM4 
            //Secret Key: 0085c57c-6b76-4ea6-849d-510ea7b9125e
            //Project ID: 5be1c470-325a-40da-8187-cdcbbe62b418

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11
                                                 | SecurityProtocolType.Tls12;
            try
            {
                var client = new MinioClient()
                    .WithEndpoint("s3.nl-ams.scw.cloud")
                    .WithRegion("nl-ams")
                    .WithCredentials("SCWPMTJRRN78RY602TM4",
                                     "0085c57c-6b76-4ea6-849d-510ea7b9125e")
                    .WithSSL()
                    .Build();

                Run(client, objFiles);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Run(MinioClient client, IFormFileCollection objFiles)
        {
            var bucketName = "resalliesbucket";

            foreach (var file in objFiles)
            {
                try
                {
                    byte[] fileBytes;
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    using var filestream = new MemoryStream(fileBytes);
                    PutObjectArgs poa = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject("Images/" + file.FileName)
                    .WithStreamData(filestream)
                    .WithObjectSize(filestream.Length)
                    .WithContentType("application/octet-stream");
                    await client.PutObjectAsync(poa);

                    StatObjectArgs stat = new StatObjectArgs()
                            .WithBucket(bucketName)
                            .WithObject("Images/" + file.FileName);

                    ObjectStat objectArgs = await client.StatObjectAsync(stat);
                }

                catch (Exception ex)
                {

                }
            }
        }
    }
}