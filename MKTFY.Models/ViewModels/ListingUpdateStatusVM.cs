﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class ListingUpdateStatusVM
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int ListingStatusId { get; set; }
    }
}