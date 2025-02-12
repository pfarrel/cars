﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json;

namespace Scraper
{
    public class JsonDumper
    {
        public void DumpAll()
        {
            using (var context = new CarsContext())
            {
                var data = context.Listings.Include("Make").Include("Model").ToList();

                DumpJson("data.js", data);
            }
        }

        public void DumpJson(string fileName, IEnumerable<Listing> stuff)
        {
            var flattened = stuff.Select(l => new
                {
                    Id = l.Id,
                    Source = l.Source.ToString(),
                    Make = l.Make.Name,
                    Model = l.Model.Name,
                    Mileage = l.Mileage,
                    Price = l.Price,
                    Year = l.Year,
                    Description = l.Description,
                    Location = l.County.ToString()
                }).ToList();
            using (FileStream fs = File.Open(fileName, FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, flattened);
            }
        }
    }
}
