﻿using MKTFY.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class Order
    {
        public Order() { }

        public Order(OrderCreateVM src)
        {
            ListingId = src.ListingId;
            BuyerId = src.BuyerId;
            Created = src.Created == null ? DateTime.UtcNow: src.Created;
            OrderStatusId = src.OrderStatusId;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ListingId { get; set; }

        // Ref to the buyer
        [Required]
        public string BuyerId { get; set; }

        public DateTime Created { get; set; }

        [Required]
        public int OrderStatusId { get; set; }

        // Navigational
        public User Buyer { get; set; }

        public Listing Listing { get; set; }

        public ListingStatus OrderStatus { get; set; }
    }
}