using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class UserCreateVM
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public int? CityId { get; set; }

        [MinLength(6), MaxLength(200)]
        public string Address { get; set; }

        [MinLength(6), MaxLength(7)]
        public string PostalCode { get; set; }
    }
}
