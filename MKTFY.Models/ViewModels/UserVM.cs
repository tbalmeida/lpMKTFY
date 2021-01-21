using MKTFY.Models.Entities;
using System.Linq;

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
            Address = src.Address;
            CityId = src.CityId;
            City = src.City.Name;
            Province = src.City.Province.Abbreviation;
            PostalCode = src.PostalCode == null ? null : src.PostalCode.Substring(0, 3) + " " + src.PostalCode.Substring(3, 3);
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? CityId { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public int QtyActiveListings { get; set; }
    }
}
