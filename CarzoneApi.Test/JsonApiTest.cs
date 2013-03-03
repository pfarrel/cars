using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarzoneApi;
using System.Threading.Tasks;

namespace CarzoneApi.Test
{
    [TestClass]
    public class JsonApiTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var api = new JsonApi();

            var responseString = await api.GetTotals();
            var a = 1;
        }
    }
}
