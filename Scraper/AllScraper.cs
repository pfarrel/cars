using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scraper.CarsIreland;
using Domain;
using System.Globalization;

namespace Scraper
{
    public class AllScraper
    {
        public void LoadCarzoneFromJson()
        {
            var carzoneApi = new CarzoneApi();

            var dumpFiles = Directory.GetFiles(@"C:\dev\cars\Scraper.Test\bin\Debug", "jsondump*");

            var carzoneListings = dumpFiles
                .AsParallel()
                .Select(path => File.ReadAllText(path))
                .SelectMany(text => carzoneApi.Deserialize(text))
                .Where(cl => cl.VehicleMake != null && cl.VehicleModel != null)
                .ToList();

            for (int i = 0; i < carzoneListings.Count(); i += 100)
            {
                using (var context = new CarsContext())
                {
                    for (int j = i; j < i + 100 && j < carzoneListings.Count(); j++)
                    {
                        var carzoneListing = carzoneListings[j];

                        var listing = new Listing(context,
                            SourceSite.Carzone,
                            carzoneListing.AdvertId.ToString(),
                            carzoneListing.VehicleMake,
                            carzoneListing.VehicleModel,
                            carzoneListing.VehicleYearOfManufacture,
                            carzoneListing.VehiclePriceEuro,
                            -1,
                            carzoneListing.AdvertiserCounty,
                            carzoneListing.VehicleDerivative);

                        context.Listings.Add(listing);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void LoadCarsIrelandFromJson()
        {
            var carsIrelandApi = new CarsIrelandApi();

            var dumpFiles = Directory.GetFiles(@"C:\dev\cars\Scraper.Test\bin\Debug", "carsirelandjsondump*");

            var listings = dumpFiles
                .AsParallel()
                .Select(path => File.ReadAllText(path))
                .SelectMany(text => carsIrelandApi.Deserialize(text))
                .ToList();

            for (int i = 0; i < listings.Count(); i += 100)
            {
                using (var context = new CarsContext())
                {
                    for (int j = i; j < i + 100 && j < listings.Count(); j++)
                    {
                        var carsIrelandListing = listings[j];

                        var culture = new CultureInfo("en-IE");
                        int price;
                        if (!int.TryParse(carsIrelandListing.Price, NumberStyles.Currency, culture, out price)) 
                        { 
                            price = -1;
                        }

                        var listing = new Listing(context,
                            SourceSite.CarsIreland,
                            carsIrelandListing.Ad_Id.ToString(),
                            carsIrelandListing.Make,
                            carsIrelandListing.Model,
                            carsIrelandListing.Reg_Year,
                            price,
                            carsIrelandListing.Mileage ?? -1,
                            carsIrelandListing.Location,
                            carsIrelandListing.Variant);

                        context.Listings.Add(listing);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
