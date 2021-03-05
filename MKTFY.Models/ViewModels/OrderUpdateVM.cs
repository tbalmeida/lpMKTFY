using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class OrderUpdateVM
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ListingId { get; set; }

        // Ref to the buyer
        [Required]
        public string BuyerId { get; set; }

        public DateTime Created { get; set; }

        public decimal Charges { get; set; }

        public decimal TotalPaid { get; set; }

        [Required]
        public int OrderStatusId { get; set; }
    }
}
