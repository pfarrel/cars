using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CarsContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Location> Locations { get; set; }

        public CarsContext() : base ("CarsContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarsContext>());
        }
    }
}
