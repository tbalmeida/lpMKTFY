using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<List<CityVM>> GetAll();

        Task<CityVM> GetById(int id);

        Task<CityVM> Create(CityCreateVM src);

        Task<CityVM> Update(int id, CityUpdateVM src);

        Task<string> Delete(int id);
    }
}
