using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IListingStatusRepository
    {
        Task<List<ListingStatusVM>> GetAll();

<<<<<<< HEAD
        Task<int> GetStatusId(string name);
=======
        Task<int> GetByName(string name);

        Task<bool> IsActive(int id);
>>>>>>> ft/transactions
    }
}
