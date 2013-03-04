using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Listing
    {
        public int Id { get; set; }

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

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public string Description { get; set; }
    }
}
