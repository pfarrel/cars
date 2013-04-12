using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Scraper.Requests;

namespace Scraper
{
    public class CarzoneApi : ApiBase
    {
        private const string BaseUrl = "http://www.carzone.ie/es-ie/search/";
        private const string CarSearchUrl = "json?startrow={0}&maxrows={1}&legacy_url=y&requestor=cz";
        private const string IndividualCarUrl = "{0}/jsonAdvert";

        private const int MaxResultsToFetchAtOnce = 100;

        public CarzoneApi(IRequester webRequester) : base(webRequester, BaseUrl)
        {
        }

        public IEnumerable<CarzoneSearchListing> GetListings(int first, int count)
        {
            var strings = GetListingsStrings(first, count);
            return strings.SelectMany(Deserialize);
        }

        public IEnumerable<string> GetListingsStrings(int first, int count)
        {
            // Do initial request just to read totals from it
            var getTotalsResponse = Requester.Get(MakeGetListingsUrl(1, 10));
            var totals = JsonConvert.DeserializeObject<CarzoneSearchResponse>(getTotalsResponse);

            var available = totals.TotalAdvertCount;
            var toFetch = Math.Min(count, (available - first) + 1);
            int currentResult = first;

            var results = new List<string>(toFetch);

            while (currentResult <= toFetch)
            {
                // +1 for case where current and available are equal, we still have to fetch one
                int fetchThisTime = Math.Min((toFetch - currentResult) + 1, MaxResultsToFetchAtOnce);
                var jsonString = Requester.Get(MakeGetListingsUrl(currentResult, fetchThisTime));
                results.Add(jsonString);
                currentResult += fetchThisTime;
            }

            return results;
        }

        private string MakeGetListingsUrl(int first, int count)
        {
            return string.Format(CarSearchUrl, first, count);
        }

        public CarzoneListingDetails GetListingDetails(long advertId)
        {
            var response = Requester.Get(string.Format(IndividualCarUrl, advertId));
            var details = JsonConvert.DeserializeObject<CarzoneListingDetails>(response);
            return details;
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

    public class CarzoneListingDetails
    {
        public long AdvertId { get; set; }
        public string AdvertDescription { get; set; }
        public string VehicleColourDescription { get; set; }
        public float VehicleEngineSizeLitres { get; set; }
        public string VehicleFuelType { get; set; }
        public int VehicleKm { get; set; }
        public int VehicleMileage { get; set; }
        public string VehicleTransmission { get; set; }
    }
}
