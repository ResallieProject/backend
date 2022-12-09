using Resallie.Controllers;
using Resallie.Services.Advertisements;
using Resallie.Models;
using Resallie.Data;
using Resallie.Respositories.Advertisements;
using Microsoft.EntityFrameworkCore;
using FakeItEasy;

namespace Resallie.Tests.Advertisement
{
    [TestFixture]
    public class AdvertisementControllerTests : BaseTests
    {
        public AdvertisementController testController;

        [OneTimeSetUp]
        public  void Init()
        {
            ///Arange
            var fakeAdvertisements = A.CollectionOfDummy<Models.Advertisement>(15).ToList();
            var testService = new AdvertisementService(new AdvertisementRepository(new AppDbContext(new DbContextOptions<AppDbContext>())), 
                    new AdvertisementFeatureRepository(new AppDbContext(new DbContextOptions<AppDbContext>())));
            var fakeService = A.Fake<AdvertisementService>();
            testController = new AdvertisementController(testService);
            //A.CallTo(() => testService.GetAll()).Returns(Task.FromResult(fakeAdvertisements));
        }

        [Test]
        ///In the test environment, the number of advertisement is set on 15.
        public void GetAllAdvertisemtensTest()
        {
            ///Act
            var count = testController;

            ///Assert
            Assert.Equals(count, 15);
        }

        [Test]
        public void CreateAdvertisementTest()
        {

        }

        [Test]
        public void DeleteAdvertisemtenTest()
        {

        }

        public void GetAdvertisemtenTest()
        {

        }

        public void UpdateAdvertisemtenTest()
        {

        }
    }
}