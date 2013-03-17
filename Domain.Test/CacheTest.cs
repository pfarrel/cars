using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Test
{
    [TestClass]
    public class CacheTest
    {
        [TestMethod]
        public void FormatCacheKey_WorksForMake()
        {
            var expected = "Make=ford";
            var actual = Cache.FormatCacheKey<Make>("Ford");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetOrCreateDimension_Creates()
        {
            var cache = new Cache(); 
            var randomName = Guid.NewGuid().ToString();
            using (var context = new CarsContext())
            {
                int id = cache.GetOrCreate<Make>(context, randomName);
                Assert.AreNotEqual(0, id);
            }
        }

        [TestMethod]
        public void GetOrCreateDimension_TwiceReturnsSame()
        {
            var cache = new Cache();
            var randomName = Guid.NewGuid().ToString();
            using (var context = new CarsContext())
            {
                int id1 = cache.GetOrCreate<Make>(context, randomName);
                int id2 = cache.GetOrCreate<Make>(context, randomName);
                Assert.AreEqual(id1, id2);
            }
        }

        [TestMethod]
        public void GetOrCreateDimension_ReturnsPreExisting()
        {
            var cache = new Cache();
            var randomName = Guid.NewGuid().ToString();

            int existingId;

            using (var context = new CarsContext())
            {
                var newMake = new Make() { Name = randomName };
                context.Makes.Add(newMake);
                context.SaveChanges();
                existingId = newMake.Id;
            }

            using (var context = new CarsContext())
            {
                int id = cache.GetOrCreate<Make>(context, randomName);
                Assert.AreEqual(existingId, id);
            }
        }
    }
}
