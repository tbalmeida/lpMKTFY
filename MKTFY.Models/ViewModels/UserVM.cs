using MKTFY.Models.Entities;
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
            CityId = src.CityId;
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? CityId { get; set; }
    }
}
