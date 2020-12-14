using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ListingId { get; set; }

        // Ref to the buyer
        [Required]
        public string UserId { get; set; }

        [Timestamp]
        public DateTime Created { get; set; }

        [Required]
        public int ListingStatusId { get; set; }

        // Navigational
        public Listing Listing { get; set; }
        public ListingStatus ListingStatus { get; set; }
        public User User { get; set; }
    }
}
