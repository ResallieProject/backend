using Minio;
using System.Net;

namespace Resallie
{
    public static class AdImagesStoreApi
    {

        public static MinioClient? AdImagesStoreClient { get; private set; }
        public static PutObjectArgs? BasePutObjectArgs { get; private set; }
        public static StatObjectArgs? BaseStatObjectArgs { get; private set; }


        public static void SetAdImagesApi(string accesKeyId, string secretKey, string bucketName)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11
                                                 | SecurityProtocolType.Tls12;
            AdImagesStoreClient = new MinioClient()
                .WithEndpoint("s3.nl-ams.scw.cloud")
                .WithRegion("nl-ams")
                .WithCredentials(accesKeyId, secretKey)
                .WithSSL()
                .Build();

            BasePutObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName);

            BaseStatObjectArgs = new StatObjectArgs()
                .WithBucket(bucketName);

        }


    }
}