using Microsoft.AspNetCore.Identity;
using MKTFY.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

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
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
