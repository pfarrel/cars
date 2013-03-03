using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarzoneApi
{
    class Car
    {
        public long AdvertId { get; set; }
        public string AdvertiserCounty { get; set; }
        public string AdvertiserName { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleDerivative { get; set; }
        public int VehiclePriceEuro { get; set; }
        public int VehicleYearManufacture { get; set; }
    }
}
