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

            for (int i = 0; i < carzoneListings.Count(); i += 100)
            {
                using (var context = new CarsContext())
                {
                    for (int j = i; j < i + 100 && j < carzoneListings.Count(); j++)
                    {
                        var carzoneListing = carzoneListings[j];

                        var make = context.Makes.SingleOrDefault(m => m.Name.ToLower() == carzoneListing.VehicleMake.ToLower());
                        if (make == null) 
                        { 
                            make = context.Makes.Add(new Make { Name = carzoneListing.VehicleMake }); 
                            context.SaveChanges(); 
                        }

                        var model = context.Models.SingleOrDefault(m => m.Name.ToLower() == carzoneListing.VehicleModel.ToLower());
                        if (model == null)
                        {
                            model  = context.Models.Add(new Model { Name = carzoneListing.VehicleModel });
                            context.SaveChanges();
                        }

                        var location = context.Locations.SingleOrDefault(m => m.Name.ToLower() == carzoneListing.AdvertiserCounty.ToLower());
                        if (location == null)
                        {
                            location = context.Locations.Add(new Location { Name = carzoneListing.AdvertiserCounty });
                            context.SaveChanges();
                        }

                        var listing = new Listing
                        {
                            Make = make,
                            Model = model,
                            Location = location,
                            Description = carzoneListing.VehicleDerivative,
                            Price = carzoneListing.VehiclePriceEuro,
                            Year = carzoneListing.VehicleYearOfManufacture
                        };
                        context.Listings.Add(listing);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
