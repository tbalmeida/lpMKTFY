using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserVM> GetUserByEmail(string email);

        // used to provide a listing count on user profile
        Task<int> ListingCount(string userId, int? statusId);

        // 
    }
}
