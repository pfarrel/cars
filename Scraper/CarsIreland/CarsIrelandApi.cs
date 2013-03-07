using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Scraper.CarsIreland
{
    public class CarsIrelandApi : ApiBase
    {
        private const string BaseUrl = "http://api.carsireland.ie/app/";

        public CarsIrelandApi() : base(BaseUrl)
        {
        }

        public string GetListingsString()
        {
            return MakeRequestSynchronous("search.json?page=0&location_id=0");
        }

        public IEnumerable<CarsIrelandSearchListing> GetListings()
        {
            var strings = GetListingsString();
            return JsonConvert.DeserializeObject<SearchResponse>(strings).Result;
        }

        public IEnumerable<CarzoneSearchListing> Deserialize(string json)
        {
            var listings = JsonConvert.DeserializeObject<CarzoneSearchResponse>(json);
            return listings.Adverts;
        }
    }

    public class SearchResponse
    {
        public List<CarsIrelandSearchListing> Result { get; set; }
        public int Total { get; set; }
    }

    public class CarsIrelandSearchListing
    {
        public int Ad_Id { get; set; }
        public string  Body_Type { get; set; }
        public string Colour { get; set; }
        public string Engine_Size { get; set; }
        public string Fuel_Type { get; set; }
        public string Location { get; set; }
        public string Make { get; set; }
        public int Mileage { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
        public int Reg_Year { get; set; }
        public string Variant { get; set; }
    }
}
