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
using Scraper.Requests;

namespace Scraper.CarsIreland
{
    public class CarsIrelandApi : ApiBase
    {
        private const string BaseUrl = "http://api.carsireland.ie/app/";

        public CarsIrelandApi(IRequester requester) : base(requester, BaseUrl)
        {
        }

        public string GetListingsString(int page = 1)
        {
            return Requester.Get(string.Format("search.json?page={0}&location_id=0", page));
        }

        public IEnumerable<CarsIrelandSearchListing> GetListings(int page = 1)
        {
            var strings = GetListingsString(page);
            return Deserialize(strings);
        }

        public IEnumerable<CarsIrelandSearchListing> Deserialize(string json)
        {
            var listings = JsonConvert.DeserializeObject<SearchResponse>(json);
            return listings.Result;
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
        public int? Mileage { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
        public int Reg_Year { get; set; }
        public string Variant { get; set; }
    }
}
