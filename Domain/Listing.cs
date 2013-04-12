using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Caching;

namespace Domain
{
    public class Listing
    {
        public int Id { get; set; }

        [Required]
        public SourceSite Source { get; set; }

        [Required]
        public string SourceId { get; set; }

        [Required]
        public int MakeId { get; set; }
        public virtual Make Make { get; set; }

        [Required]
        public int ModelId { get; set; }
        public virtual Model Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

        public int EngineSize { get; set; }

        public int Mileage { get; set; }

        [Required]
        public County County { get; set; }

        public string Description { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateLastSeen { get; set; }

        public Listing()
        {
        }

        public Listing(
            CarsContext context,
            Cache cache,
            SourceSite source,
            string sourceId,
            string make,
            string model,
            int year,
            int price,
            int mileage,
            string location,
            string description)
        {
            Active = true;
            DateAdded = DateTime.Now;
            DateLastSeen = DateTime.Now;

            Source = source;
            SourceId = sourceId;
            Year = year;
            Price = price;
            Mileage = mileage;
            Description = description;

            MakeId = cache.GetOrCreate<Make>(make, () => context.Set<Make>().GetOrCreate(make));
            ModelId = cache.GetOrCreate<Model>(model, () => context.Set<Model>().GetOrCreate(model));

            County = EnumHelpers.FromString<County>(location);
        }
    }
}
