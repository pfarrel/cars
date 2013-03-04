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
        public void Scrape_Works()
        {
            var scraper = new AllScraper();

            scraper.Scrape();

            using (var context = new CarsContext())
            {
                Assert.IsTrue(context.Listings.ToList().Count > 0);
            }
        }

        [TestMethod]
        public void Scrape2_Works()
        {
            var scraper = new AllScraper();

            scraper.Scrape2();

            using (var context = new CarsContext())
            {
                Assert.IsTrue(context.Listings.ToList().Count > 0);
            }
        }
    }
}
