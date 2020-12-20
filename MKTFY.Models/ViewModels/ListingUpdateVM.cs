﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class ListingUpdateVM
    {
        [Required]    
        public Guid Id { get; set; }

        [Required, MaxLength(200), MinLength(10)]
        public string Title { get; set; }

        [Required, MaxLength(1000), MinLength(10)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public decimal Price { get; set; }

        // Normally it'll be the user's preferred city, but it's possible to publish somewhere else
        [Required]
        public int CityId { get; set; }

        [Required]
        public int ListingStatusId { get; set; }

        [Timestamp]
        public DateTime Created { get; set; }

        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
