using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<CompanyVM> Create(CompanyCreateVM src);

        Task<List<CompanyVM>> GetAll();
    }
}
