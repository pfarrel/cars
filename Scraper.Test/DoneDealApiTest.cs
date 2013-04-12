using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scraper.CarsIreland;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;

namespace Scraper.Test
{
    [TestClass]
    public class DoneDealApiTest
    {
        DoneDealApi api = new DoneDealApi(new Requests.WebRequester());

        [TestMethod]
        public void GetSearchResultsString_LooksRight()
        {
            var responseString = api.GetSearchResultsString(0);
            Assert.IsTrue(responseString.Contains("sectionDisplay\":\"Cars\""));
        }

        [TestMethod]
        public void GetSearchResults_Deserializes()
        {
            var response = api.GetSearchResults(0);
            Assert.IsTrue(response.TotalCount > 10000);
            Assert.IsTrue(response.Ads.Count() == 15);
        }

        [TestMethod]
        public void GetListingString_LooksRight()
        {
            var responseString = api.GetListingString(4861950);
            Assert.IsTrue(responseString.Contains("mileage_metric"));
        }

        [TestMethod]
        public void GetListing_DeserializesAllValues()
        {
            var listing = api.GetListing(4861950);

            Assert.IsTrue(!string.IsNullOrEmpty(listing.County));
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Description));
            Assert.AreNotEqual(0, listing.Price);
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Make));
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Model));
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Mileage_Metric));
            Assert.AreNotEqual(0, listing.Year);
            Assert.AreNotEqual(0, listing.Mileage);
//            Assert.IsTrue(!string.IsNullOrEmpty(listing.FuelType));
        }

        [TestMethod]
        public void GetXDetails_Gets()
        {
            var listings = api.GetXDetails(1);
            Assert.IsTrue(listings.Count() == 15);
        }
    }
}
