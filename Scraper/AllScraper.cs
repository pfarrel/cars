using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

            var cars = carzoneApi.GetCarsDeserialize(int.MaxValue).Result;

            using (var context = new CarsContext())
            {
                var listings = cars
                    .Where(c => c.VehicleMake != null && c.VehicleModel != null)
                    .Select(car => new Listing
                    {
                        Make = context.Makes.SingleOrDefault(m => m.Name == car.VehicleMake) ?? new Make { Name = car.VehicleMake },
                        Model = context.Models.SingleOrDefault(m => m.Name == car.VehicleModel) ?? new Model { Name = car.VehicleModel },
                        Description = car.VehicleDerivative,
                        Price = car.VehiclePriceEuro
                    }).ToList();

                foreach (var listing in listings) { context.Listings.Add(listing); }

                context.SaveChanges();
            }
        }
    }
}
