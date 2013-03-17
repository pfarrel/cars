using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class Dimension
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }

    public static class DimensionExtensions
    {
        public static int GetOrCreate<T>(this DbSet<T> dbSet, string name) where T : Dimension, new()
        {
            T dimension;
            dimension = dbSet.SingleOrDefault(d => d.Name.ToLower() == name.ToLower());
            if (dimension == null)
            {
                dimension = new T() { Name = name };
                using (var creationContext = new CarsContext())
                {
                    creationContext.Set<T>().Add(dimension);
                    creationContext.SaveChanges();
                }
            }

            return dimension.Id;
        }
    }
}
