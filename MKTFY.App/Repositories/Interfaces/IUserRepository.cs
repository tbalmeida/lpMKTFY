using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserVM> GetUserByEmail(string email);

        // used to provide a listing count on user profile
        Task<int> ListingCount(string userId, bool activeOnly, [Optional] int? statusId);

        // Show account info
        Task<UserVM> GetById(string id);
        // Update address

        // Logout

        // Change password
    }
}
