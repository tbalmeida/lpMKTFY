﻿using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class Company
    {
        public Company() { }

        public Company(CompanyCreateVM src)
        {
            Name = src.Name;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public String Name { get; set; }
    }
}
