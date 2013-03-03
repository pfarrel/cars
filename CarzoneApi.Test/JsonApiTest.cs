using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarzoneApi;
using System.Threading.Tasks;

namespace CarzoneApi.Test
{
    [TestClass]
    public class JsonApiTest
    {
        JsonApi api = new JsonApi();
        [TestMethod]
        public async Task GetMakeModels_LooksRight()
        {
            var responseString = await api.GetMakeModels();
            Assert.IsTrue(responseString.Contains("Lexus"));
        }

        [TestMethod]
        public async Task GetCars_LooksRight()
        {
            var responseString = await api.GetCars();
            Assert.IsTrue(responseString.Contains("vehicle"));
        }

        [TestMethod]
        public async Task GetCars_JsWorks()
        {
            var cars = await api.GetCarsDS();
            Assert.IsTrue(cars.Count() > 0);
        }

        [TestMethod]
        public async Task GetCars_JsDeserializesAllValues()
        {
            var cars = await api.GetCarsDS();
            var car = cars.First();

            Assert.IsTrue(car.AdvertId > 1000);
            Assert.IsTrue(!string.IsNullOrEmpty(car.AdvertiserCounty));
            Assert.IsTrue(!string.IsNullOrEmpty(car.AdvertiserName));
            Assert.IsTrue(!string.IsNullOrEmpty(car.VehicleDerivative));
            Assert.IsTrue(!string.IsNullOrEmpty(car.VehicleMake));
            Assert.IsTrue(!string.IsNullOrEmpty(car.VehicleModel));
            Assert.IsTrue(car.VehicleYearOfManufacture > 1900);

            //Assert.IsTrue(car.VehiclePriceEuro > 0);
        }
    }
}
