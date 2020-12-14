using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class ListingImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid ListingId { get; set; }

        [Required]
        public byte[] Picture { get; set; }

        [Required]
        public bool MainPic { get; set; }


        // Navigational
        public Listing Listing { get; set; }
    }
}
