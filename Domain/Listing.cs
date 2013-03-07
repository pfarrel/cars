using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int Mileage { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public string Description { get; set; }

        public Listing()
        {
        }

        public Listing(
            CarsContext context,
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
            Source = source;
            SourceId = sourceId;
            Year = year;
            Price = price;
            Mileage = mileage;
            Description = description;

            bool newEntityCreated = false;
            var makeEntity = context.Makes.SingleOrDefault(m => m.Name.ToLower() == make.ToLower());
            if (makeEntity == null)
            {
                makeEntity = context.Makes.Add(new Make { Name = make });
                newEntityCreated = true;
            }
            Make = makeEntity;

            var modelEntity = context.Models.SingleOrDefault(m => m.Name.ToLower() == model.ToLower());
            if (modelEntity == null)
            {
                modelEntity = context.Models.Add(new Model { Name = model });
                newEntityCreated = true;
            }
            Model = modelEntity;

            var locationEntity = context.Locations.SingleOrDefault(m => m.Name.ToLower() == location.ToLower());
            if (locationEntity == null)
            {
                locationEntity = context.Locations.Add(new Location { Name = location });
                newEntityCreated = true;
            }
            Location = locationEntity;

            if (newEntityCreated)
            {
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Console.Out.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
        }
    }
}
