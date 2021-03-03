using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IFeeRepository
    {
        Task<FeeVM> Create(FeeCreateVM src);

        Task<FeeVM> Update(FeeUpdateVM src);

        Task<List<FeeVM>> GetAll(bool onlyActive = true);

        Task<FeeVM> GetById(int id);

        Task<string> Delete(int id);

        Task<decimal> GetCharges(decimal itemPrice);
    }
}
