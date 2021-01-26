using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class UserUpdateVM
    {
        [Required]
        public string Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int? CityId { get; set; }

        [MinLength(6), MaxLength(200)]
        public string Address { get; set; }

        [MinLength(6), MaxLength(7)]
        public string PostalCode { get; set; }
    }
}
