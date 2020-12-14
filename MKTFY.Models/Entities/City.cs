using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class City
    {
        public City() { }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50), MinLength(3)]
        public string Name { get; set; }

        [Required]
        public int ProvinceId { get; set; }

        // Navigational
        public Province Province { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Listing> Listings { get; set; }
    }
}
