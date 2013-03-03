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

        public async Task<string> GetTotals()
        {
            var reqAddress = BaseUrl + "makeModelsJs?getResponseAsJson=true&requestor=cz";
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(reqAddress);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
