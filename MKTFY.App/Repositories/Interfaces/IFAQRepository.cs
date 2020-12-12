using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IFAQRepository
    {
        Task<FAQVM> Create(FAQCreateVM src);

        //Task<FAQVM> Update(FAQUpdateVM src);

        Task<string> Delete(Guid id);

        Task<List<FAQVM>> FilterFAQ(string searchTerm = null);

        Task<List<FAQVM>> GetAll();

        Task<FAQVM> GetById(Guid id);
    }
}
