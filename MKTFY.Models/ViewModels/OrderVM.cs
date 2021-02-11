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
            OrderStatus = src.OrderStatus?.Name;
            if (src.Listing != null)
            {
                Title = src.Listing.Title;
                Price = src.Listing.Price;
                SellerId = src.Listing.UserId;
            }
            Category = src.Listing.Category?.Title;
            Seller = src.Listing.User?.FirstName + " " + src.Listing.User?.LastName;
        }

        public Guid Id { get; set; }

        // Listing data
        public Guid ListingId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string ItemCondition { get; set; }
        public decimal? Price { get; set; }

        // Seller
        public string SellerId { get; set; }
        public string Seller { get; set; }

        public string BuyerId { get; set; }

        public int OrderStatusId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime Created { get; set; }
    }
}
