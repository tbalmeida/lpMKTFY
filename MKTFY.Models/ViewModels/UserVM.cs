﻿using MKTFY.Models.Entities;
using System;

namespace MKTFY.Models.ViewModels
{
    public class UserVM
    {
        public UserVM(User src)
        {
            Id = src.Id;
            Email = src.Email;
            FirstName = src.FirstName;
            LastName = src.LastName;
        }

        public String Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
