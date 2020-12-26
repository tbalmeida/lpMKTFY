using MKTFY.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IItemConditionRepository
    {
        Task<List<ItemConditionVM>> GetAll();
    }
}
