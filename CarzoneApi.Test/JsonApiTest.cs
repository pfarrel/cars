using System;
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
        public async Task GetTotals_LooksRight()
        {
            var responseString = await api.GetTotals();
            Assert.IsTrue(responseString.Contains("Lexus"));
        }

        [TestMethod]
        public async Task GetCars_LooksRight()
        {
            var responseString = await api.GetCars();
            Assert.IsTrue(responseString.Contains("vehicle"));
        }

    }
}
