using System;
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
                var data = context.Listings.ToList();

                DumpJson("data.js", data);
            }
        }

        public void DumpJson(string fileName, IEnumerable<Listing> stuff)
        {
            var flattened = stuff.Select(l => new
                        {
                            Make = l.Make.Name,
                            Model = l.Model.Name,
                            Price = l.Price,
                            Description = l.Price,
                        }).ToList();
            using (FileStream fs = File.Open(fileName, FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, stuff);
            }
        }
    }
}
