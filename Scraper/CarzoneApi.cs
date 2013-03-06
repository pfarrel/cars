using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Scraper
{
    public class CarzoneApi
    {
        private const string BaseUrl = "http://www.carzone.ie/es-ie/search/";
        private const string GetCarsFormatSuffix = "json?startrow={0}&maxrows={1}&legacy_url=y&requestor=cz";

        private const int MaxResultsToFetchAtOnce = 100;

        private HttpClient client;

        public CarzoneApi()
        {
            client = new HttpClient();
        }

        public IEnumerable<CarzoneSearchListing> GetListings(int first, int count)
        {
            var strings = GetListingsStrings(first, count);
            return strings.SelectMany(Deserialize);
        }

        public IEnumerable<string> GetListingsStrings(int first, int count)
        {
            // Do initial request just to read totals from it
            var getTotalsResponse = MakeRequest(MakeGetListingsUrl(1, 10));
            var totals = JsonConvert.DeserializeObject<CarzoneSearchResponse>(getTotalsResponse);

            var available = totals.TotalAdvertCount;
            var toFetch = Math.Min(count, (available - first) + 1);
            int currentResult = first;

            var results = new List<string>(toFetch);

            while (currentResult <= toFetch)
            {
                // +1 for case where current and available are equal, we still have to fetch one
                int fetchThisTime = Math.Min((toFetch - currentResult) + 1, MaxResultsToFetchAtOnce);
                var jsonString = MakeRequest(MakeGetListingsUrl(currentResult, fetchThisTime));
                results.Add(jsonString);
                currentResult += fetchThisTime;
            }

            return results;
        }

        private string MakeRequest(string url)
        {
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                throw new ApplicationException("Request failed - rate limited or blocked (204 No Content)");
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        private string MakeGetListingsUrl(int first, int count)
        {
            return BaseUrl + string.Format(GetCarsFormatSuffix, first, count);
        }

        public IEnumerable<CarzoneSearchListing> Deserialize(string json)
        {
            var listings = JsonConvert.DeserializeObject<CarzoneSearchResponse>(json);
            return listings.Adverts;
        }
    }

    public class CarzoneSearchResponse
    {
        public int TotalAdvertCount { get; set; }
        public List<CarzoneSearchListing> Adverts { get; set; }
    }

    public class CarzoneSearchListing
    {
        public long AdvertId { get; set; }
        public string AdvertiserCounty { get; set; }
        public string AdvertiserName { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleDerivative { get; set; }
        public int VehiclePriceEuro { get; set; }
        public int VehicleYearOfManufacture { get; set; }
    }
}
