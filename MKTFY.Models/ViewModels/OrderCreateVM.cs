using System;

namespace MKTFY.Models.ViewModels
{
    public class OrderCreateVM
    {
        public Guid ListingId { get; set; }

        public string BuyerId { get; set; }

        public int OrderStatusId { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public decimal Charges { get; set; }

        public decimal TotalPaid { get; set; }
    }
}
