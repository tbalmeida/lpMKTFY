using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryVM> Create(CategoryCreateVM src);

        Task<CategoryVM> Update(int Id, CategoryUpdateVM src);

        Task<List<CategoryVM>> GetAll();

        Task<CategoryVM> GetById(int id);

        Task<string> Delete(int id);
    }
}
