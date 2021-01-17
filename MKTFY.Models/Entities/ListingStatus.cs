using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class ListingStatus
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string Name { get; set; }

        public bool IsActive { get; set; } = false;

        public ICollection<Listing> Listings { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
