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
                new Listing { Make = fiat, Model = panda, Year = 2010, Price = 1000 },
                new Listing { Make = fiat, Model = panda, Year = 2010, Price = 2000 },
                new Listing { Make = fiat, Model = panda, Year = 2011, Price = 3000 },
                new Listing { Make = fiat, Model = panda, Year = 2011, Price = 4000 },
                new Listing { Make = fiat, Model = panda, Year = 2013, Price = 5000 },

                new Listing { Make = fiat, Model = doblo, Year = 2007, Price = 1500 },
                new Listing { Make = fiat, Model = doblo, Year = 2008, Price = 2500 },
                new Listing { Make = fiat, Model = doblo, Year = 2009, Price = 3500 },
                new Listing { Make = fiat, Model = doblo, Year = 2010, Price = 4500 },
                new Listing { Make = fiat, Model = doblo, Year = 2011, Price = 5500 },

                new Listing { Make = renault, Model = megane, Year = 2005, Price = 750 },
                new Listing { Make = renault, Model = megane, Year = 2006, Price = 1750 },
                new Listing { Make = renault, Model = megane, Year = 2009, Price = 2750 },
                new Listing { Make = renault, Model = megane, Year = 2009, Price = 3750 },
                new Listing { Make = renault, Model = megane, Year = 2011, Price = 4750 },

                new Listing { Make = renault, Model = clio, Year = 2003, Price = 200 },
                new Listing { Make = renault, Model = clio, Year = 2004, Price = 1200 },
                new Listing { Make = renault, Model = clio, Year = 2007, Price = 2200 },
                new Listing { Make = renault, Model = clio, Year = 2005, Price = 3200 },
                new Listing { Make = renault, Model = clio, Year = 2006, Price = 4200 },
            };

            dumper.DumpJson("dump.json", listings);
        }
    }
}
