using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resallie.Respositories.Advertisements;

namespace Resallie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestUpload : ControllerBase
    {
        private readonly AdvertisementImagesRepository _repository;

        public TestUpload(AdvertisementImagesRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public void TestUploadMethod(IFormFileCollection collection)
        {
            _repository.StoreImages(null, collection);
        }
    }
}
