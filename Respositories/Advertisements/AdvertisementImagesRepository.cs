    using Minio;
using Minio.Exceptions;
using Resallie.Data;
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

        //[Obsolete]
        public async Task StoreImages(Advertisement advertisement, IFormFileCollection objFiles)
        {
            //Acces Key ID: SCWPMTJRRN78RY602TM4 
            //Secret Key: 0085c57c-6b76-4ea6-849d-510ea7b9125e
            //Project ID: 5be1c470-325a-40da-8187-cdcbbe62b418
            try
            {
                var minio = new MinioClient(
                    "s3.nl-ams.scw.cloud",
                    "SCWPMTJRRN78RY602TM4",
                    "0085c57c-6b76-4ea6-849d-510ea7b9125e",
                    "nl-ams"
                    ).WithSSL();

                string[] fileEntries = Directory.GetFiles("/app");
                foreach (string fileName in fileEntries)
                {
                    Run(minio, fileName).Wait();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async static Task Run(MinioClient minio, string file)
        {
            try
            {
                await minio.PutObjectAsync("my-bucket", Path.GetFileName(file), file, "");
                Console.WriteLine("Successfully uploaded " + file);
            }

            catch (MinioException e)
            {
                Console.WriteLine("File Upload Error: {0}", e.Message);
            }
        }
    }
}
