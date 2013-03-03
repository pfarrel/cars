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
        private const string GetCarsFormatSuffix = "json?startrow={0}&maxrows={1}&legacy_url=y&requestor=cz";

        private string MakeModelsUrl { get { return BaseUrl + MakeModelsSuffix; } }

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
            var response = await client.GetAsync(MakeGetCarsUrl());

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private string MakeGetCarsUrl(int first = 1, int count = 10)
        {
            return BaseUrl + string.Format(GetCarsFormatSuffix, first, count);
        }

        public async Task<IEnumerable<Car>> GetCarsDS()
        {
            var response = client.GetStringAsync(MakeGetCarsUrl());
            var responseWrapped = await JsonConvert.DeserializeObjectAsync<CarListResponse>(response.Result);
            return responseWrapped.Adverts;
        }


    }
}
