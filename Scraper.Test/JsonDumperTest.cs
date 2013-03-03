using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scraper.Test
{
    [TestClass]
    public class JsonDumperTest
    {
        JsonDumper dumper = new JsonDumper();

        [TestMethod]
        public void Dumper_Works()
        {
            dumper.DumpAll();
        }

        [TestMethod]
        public void Dumper_TempTest()
        {
            var fiat = new Make { Name = "Fiat" };
            var panda = new Model { Name = "Panda" };
            var doblo = new Model { Name = "Doblo" };

            var renault = new Make { Name = "Renault" };
            var megane = new Model { Name = "Megane" };
            var clio = new Model { Name = "Clio" };

            var listings = new List<Listing>()
            {
                new Listing { Make = fiat, Model = panda, Price = 1000 },
                new Listing { Make = fiat, Model = panda, Price = 2000 },
                new Listing { Make = fiat, Model = panda, Price = 3000 },
                new Listing { Make = fiat, Model = panda, Price = 4000 },
                new Listing { Make = fiat, Model = panda, Price = 5000 },

                new Listing { Make = fiat, Model = doblo, Price = 1500 },
                new Listing { Make = fiat, Model = doblo, Price = 2500 },
                new Listing { Make = fiat, Model = doblo, Price = 3500 },
                new Listing { Make = fiat, Model = doblo, Price = 4500 },
                new Listing { Make = fiat, Model = doblo, Price = 5500 },

                new Listing { Make = renault, Model = megane, Price = 750 },
                new Listing { Make = renault, Model = megane, Price = 1750 },
                new Listing { Make = renault, Model = megane, Price = 2750 },
                new Listing { Make = renault, Model = megane, Price = 3750 },
                new Listing { Make = renault, Model = megane, Price = 4750 },

                new Listing { Make = renault, Model = clio, Price = 200 },
                new Listing { Make = renault, Model = clio, Price = 1200 },
                new Listing { Make = renault, Model = clio, Price = 2200 },
                new Listing { Make = renault, Model = clio, Price = 3200 },
                new Listing { Make = renault, Model = clio, Price = 4200 },
            };

            dumper.DumpJson("dump.json", listings);
        }
    }
}
