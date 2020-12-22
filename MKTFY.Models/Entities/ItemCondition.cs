using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class ItemCondition
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(20)]
        [Required]
        public string Name { get; set; }

        // Navigational
        public ICollection<Listing> Listings { get; set; }
    }
}
