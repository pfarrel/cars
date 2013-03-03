using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace CarzoneApi
{
    public class JsonApi
    {
        private const string BaseUrl = "http://www.carzone.ie/es-ie/search/";
        private const string MakeModelsUrl = "makeModelsJs?getResponseAsJson=true&requestor=cz";
        private const string GetCarsFormatUrl = "/es-ie/search/json?startrow=1&maxrows=10&legacy_url=y&requestor=cz";

        public async Task<string> GetTotals()
        {
            var reqAddress = BaseUrl + MakeModelsUrl;
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(reqAddress);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetCars()
        {
            var reqAddress = BaseUrl + GetCarsFormatUrl;
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(reqAddress);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
