using MKTFY.Models.ViewModels;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserVM> GetUserByEmail(string email);
    }
}
