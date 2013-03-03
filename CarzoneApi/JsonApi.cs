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
        private const string MakeModelsSuffix = "makeModelsJs?getResponseAsJson=true&requestor=cz";
        private const string GetCarsFormatSuffix = "json?startrow=1&maxrows=10&legacy_url=y&requestor=cz";

        private string MakeModelsUrl { get { return BaseUrl + MakeModelsSuffix; } }
        private string GetCarsUrl { get { return BaseUrl + GetCarsFormatSuffix; } }

        private HttpClient client;

        public JsonApi()
        {
            client = new HttpClient();
        }

        public async Task<string> GetMakeModels()
        {
            var response = await client.GetAsync(MakeModelsUrl);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetCars()
        {
            var response = await client.GetAsync(GetCarsUrl);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
