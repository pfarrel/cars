using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scraper.Requests;

namespace Scraper
{
    public class DoneDealApi : ApiBase
    {
        private const string BaseUrl = "http://api.donedeal.ie/api/";

        public DoneDealApi(IRequester webRequester) : base(webRequester, BaseUrl)
        {
        }

        public string GetSearchResultsString(int page)
        {
            var payload = JsonConvert.SerializeObject(new { start = page, source = new[] { "private", "trade" } });
            var response = Requester.PostAsync("find/cars/for-sale/Ireland/", payload).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public DoneDealSearchResponse GetSearchResults(int page)
        {
            var responseString = GetSearchResultsString(page);
            var response = JsonConvert.DeserializeObject<DoneDealSearchResponse>(responseString);
            return response;
        }

        public string GetListingString(long listingId)
        {
            var response = Requester.Get("view/ad/" + listingId);
            return response;
        }

        public DoneDealDetails GetListing(long listingId)
        {
            var response = GetListingString(listingId);
            return DoneDealDetails.FromJsonString(response);
        }

        public IEnumerable<DoneDealDetails> GetXDetails(int count)
        {
            int numReqs = ((count - 1) / 15) + 1;
            for (int i = 0; i < numReqs; i++)
            {
                var searchResponse = GetSearchResults(i);
                foreach (var listing in searchResponse.Ads)
                {
                    yield return GetListing(listing.Id);
                }
            }
        }
    }

    public class DoneDealSearchResponse
    {
        public int TotalCount { get; set; }
        public int PrivateCount { get; set; }
        public int TradeCount { get; set; }

        public List<DoneDealListing> Ads { get; set; }
    }

    public class DoneDealListing
    {
        public long Id { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class DoneDealDetails
    {
        [JsonProperty]
        public string County { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public int Price { get; set; }
        [JsonProperty]
        public List<DDAttrib> Attributes { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Mileage_Metric { get; set; }
        public Decimal? EngineSize { get; set; }
        public string FuelType { get; set; }

        public static DoneDealDetails FromJsonString(string json)
        {
            var jObject = JObject.Parse(json);
            var inner = jObject["ad"];

            var listing = JsonConvert.DeserializeObject<DoneDealDetails>(inner.ToString());
            listing.Make = listing.Attributes.Single(a => a.Name == "make").Value;
            listing.Model = listing.Attributes.Single(a => a.Name == "model").Value;
            listing.Year = Int32.Parse(listing.Attributes.Single(a => a.Name == "year").Value);
            listing.Mileage_Metric = listing.Attributes.Where(a => a.Name == "mileage_metric").Select(a => a.Value).SingleOrDefault();
            //listing.EngineSize = Decimal.Parse(listing.Attributes.Single(a => a.Name == "engine").Value);
            //listing.FuelType = listing.Attributes.Single(a => a.Name == "fuelType").Value;

            var mileage = listing.Attributes.SingleOrDefault(a => a.Name == "mileage");
            listing.Mileage = (mileage != null) ? Int32.Parse(mileage.Value) : -1;

            return listing;
        }

        public class DDAttrib
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
