using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKTFY.Models.Entities
{
    public class Listing
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(200), MinLength(10)]
        public string Title { get; set; }

        [Required, MaxLength(1000), MinLength(10)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Column("owner"), Required]
        public string UserId { get; set; }

        // Normally it'll be the user's preferred city, but it's possible to publish somewhere else
        [Required]
        public int CityId { get; set; }

        [Required]
        [Column("status")]
        public int ListingStatusId { get; set; }

        [Timestamp]
        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        // Navigational
        public User User { get; set; }

        public City City { get; set; }

        public Category Category { get; set; }

        public ListingStatus ListingStatus { get; set; }

        public ICollection<ListingImage> ListingImages { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
