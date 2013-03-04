using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarzoneApi;
using Domain;

namespace Scraper
{
    public class AllScraper
    {
        public void Scrape()
        {
            JsonApi carzoneApi = new JsonApi();

            var cars = carzoneApi.GetListings(1, 1000);

            using (var context = new CarsContext())
            {
                foreach (var car in cars)
                {
                    var listing = new Listing
                        {
                            Make = context.Makes.SingleOrDefault(m => m.Name == car.VehicleMake) ?? new Make { Name = car.VehicleMake },
                            Model = context.Models.SingleOrDefault(m => m.Name == car.VehicleModel) ?? new Model { Name = car.VehicleModel },
                            Description = car.VehicleDerivative,
                            Price = car.VehiclePriceEuro
                        };
                    context.Listings.Add(listing);
                    context.SaveChanges();
                }
            }
        }

        public void Scrape2()
        {
            JsonApi carzoneApi = new JsonApi();

            var dumpFiles = Directory.GetFiles(@"C:\dev\cars\CarzoneApi.Test\bin\Debug", "jsondump*");

            var carzoneListings = new List<CarzoneListing>(50000);
            foreach (var path in dumpFiles)
            {
                var text = File.ReadAllText(path);
                var ds = carzoneApi.Deserialize(text);
                carzoneListings.AddRange(ds);
            }

            carzoneListings = carzoneListings.Where(cl => cl.VehicleMake != null && cl.VehicleModel != null).ToList();

            using (var context = new CarsContext())
            {
                foreach (var carzoneListing in carzoneListings)
                {
                    var listing = new Listing
                    {
                        Make = context.Makes.SingleOrDefault(m => m.Name == carzoneListing.VehicleMake) ?? new Make { Name = carzoneListing.VehicleMake },
                        Model = context.Models.SingleOrDefault(m => m.Name == carzoneListing.VehicleModel) ?? new Model { Name = carzoneListing.VehicleModel },
                        Description = carzoneListing.VehicleDerivative,
                        Price = carzoneListing.VehiclePriceEuro
                    };
                    context.Listings.Add(listing);
                    context.SaveChanges();
                }
            }
        }
    }
}
