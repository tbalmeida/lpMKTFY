using Microsoft.AspNetCore.Identity;
using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKTFY.Models.Entities
{
    public class User: IdentityUser
    {
        public User() { }

        public User(UserCreateVM src)
        {
            FirstName = src.FirstName;
            LastName = src.LastName;
            Email = src.Email;
            CityId = src.CityId;
            Address = src.Address;
            PostalCode = src.PostalCode;
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? CityId { get; set; }

        [MinLength(6), MaxLength(200)]
        public string Address { get; set; }

        [MinLength(6), MaxLength(7)]
        public string PostalCode { get; set; }

        // Navigational
        public City City { get; set; }
        public ICollection<Listing> Listings { get; set; }

        [InverseProperty("Buyer")]
        public ICollection<Order> Orders { get; set; }
    }
}