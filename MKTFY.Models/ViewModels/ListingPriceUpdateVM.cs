using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class ListingPriceUpdateVM
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
