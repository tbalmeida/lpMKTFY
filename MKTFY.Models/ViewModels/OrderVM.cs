using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class OrderVM
    {
        public OrderVM(Order src)
        {
            Id = src.Id;
            ListingId = src.ListingId;
            BuyerId = src.BuyerId;
            Created = src.Created;
            OrderStatusId = src.OrderStatusId;
        }

        public Guid Id { get; set; }

        public Guid ListingId { get; set; }

        public string BuyerId { get; set; }

        public int OrderStatusId { get; set; }

        public DateTime Created { get; set; }
    }
}
