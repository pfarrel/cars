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

        public CarsContext() : base ("CarsContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarsContext>());
        }
    }
}
