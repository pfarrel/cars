using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

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
        public void GetOrCreateMake_CallWhenNotInCache()
        {
            var cache = new Cache();
            var mockGetOrCreate = MockRepository.GenerateMock<Func<int>>();

            cache.GetOrCreate<Make>("_", mockGetOrCreate);

            mockGetOrCreate.AssertWasCalled(a => a.Invoke());
        }

        //[TestMethod]
        //public void GetOrCreateDimension_TwiceReturnsSame()
        //{
        //    var cache = new Cache();
        //    var randomName = Guid.NewGuid().ToString();

        //        int id1 = cache.GetOrCreate<Make>(randomName, context, );
        //        int id2 = cache.GetOrCreate<Make>(context, randomName);
        //        Assert.AreEqual(id1, id2);
        //}

        //[TestMethod]
        //public void GetOrCreateDimension_ReturnsPreExisting()
        //{
        //    var cache = new Cache();
        //    var randomName = Guid.NewGuid().ToString();

        //    int existingId;

        //    using (var context = new CarsContext())
        //    {
        //        var newMake = new Make() { Name = randomName };
        //        context.Makes.Add(newMake);
        //        context.SaveChanges();
        //        existingId = newMake.Id;
        //    }

        //    using (var context = new CarsContext())
        //    {
        //        int id = cache.GetOrCreate<Make>(context, randomName);
        //        Assert.AreEqual(existingId, id);
        //    }
        //}
    }
}
