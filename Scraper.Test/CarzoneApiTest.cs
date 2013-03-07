using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scraper;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Scraper.Test
{
    [TestClass]
    public class CarzoneApiTest
    {
        CarzoneApi api = new CarzoneApi();

        [TestMethod]
        public void GetListingsStrings_LooksRight()
        {
            var responseString = api.GetListingsStrings(1, 1);
            Assert.IsTrue(responseString.Contains("vehicle"));
        }

        [TestMethod]
        public void GetListings_Deserializes()
        {
            var cars = api.GetListings(1, 10);
            Assert.IsTrue(cars.Count() == 10);
        }

        [TestMethod]
        public void GetListings_DeserializesAllValues()
        {
            var cars = api.GetListings(1, 1);
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

        [TestMethod]
        public void GetListings_SaveApiOutput()
        {
            var strings = api.GetListingsStrings(1, int.MaxValue).ToList();

            for (int i = 0; i < strings.Count(); i++)
            {
                using (var tw = File.CreateText(string.Format("jsondump{0}.json", i)))
                {
                    tw.Write(strings[i]);
                }
            }
        }
    }
}
