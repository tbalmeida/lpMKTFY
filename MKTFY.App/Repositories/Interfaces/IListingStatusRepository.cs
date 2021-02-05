using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IListingStatusRepository
    {
        Task<List<ListingStatusVM>> GetAll();

        Task<int> GetStatusId(string name);
    }
}
