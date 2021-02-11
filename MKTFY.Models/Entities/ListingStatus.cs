using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKTFY.Models.Entities
{
    public class ListingStatus
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string Name { get; set; }

        public bool IsActive { get; set; } = false;

        // Navigational
        public ICollection<Listing> Listings { get; set; }
        
        [InverseProperty("OrderStatus")]
        public ICollection<Order> Orders { get; set; }
    }
}
