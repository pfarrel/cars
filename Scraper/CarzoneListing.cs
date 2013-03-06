using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    public class CarzoneJsonResponse
    {
        public int TotalAdvertCount { get; set; }
        public List<CarzoneListing> Adverts { get; set; }
    }

    public class CarzoneListing
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
