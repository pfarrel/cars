using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scraper.CarsIreland;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Scraper.Test
{
    [TestClass]
    public class CarsIrelandApiTest
    {
        CarsIrelandApi api = new CarsIrelandApi();

        [TestMethod]
        public void GetListingsStrings_LooksRight()
        {
            var responseString = api.GetListingsString();
            Assert.IsTrue(responseString.Contains("body_type"));
        }

        [TestMethod]
        public void GetListings_Deserializes()
        {
            var listings = api.GetListings();
            Assert.IsTrue(listings.Count() > 0);
        }

        [TestMethod]
        public void GetListings_DeserializesAllValues()
        {
            var listings = api.GetListings();
            var listing = listings.First();

            Assert.IsTrue(listing.Ad_Id > 1000);
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Body_Type));
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Colour));
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Engine_Size));
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Fuel_Type));
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Location));
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Make));
            Assert.IsTrue(listing.Mileage > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Model));
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Price));
            Assert.IsTrue(listing.Reg_Year > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Variant));
        }
    }
}
