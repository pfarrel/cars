using System;
using System.Linq;
using System.Data.Entity;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scraper;

namespace Scraper.Test
{
    [TestClass]
    public class AllScraperTest
    {
        [TestMethod]
        public void LoadCarzoneFromJson_Works()
        {
            var scraper = new AllScraper();

            scraper.LoadCarzoneFromJson();

            using (var context = new CarsContext())
            {
                Assert.IsTrue(context.Listings.ToList().Count > 0);
            }
        }

        [TestMethod]
        public void LoadCarsIrelandFromJson_Works()
        {
            var scraper = new AllScraper();

            scraper.LoadCarsIrelandFromJson();

            using (var context = new CarsContext())
            {
                Assert.IsTrue(context.Listings.ToList().Count > 0);
            }
        }
    }
}
