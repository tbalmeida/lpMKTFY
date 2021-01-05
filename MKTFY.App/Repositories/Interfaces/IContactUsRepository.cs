using MKTFY.Models.ViewModels;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IContactUsRepository
    {
        public Task<bool> SendEmailMsg(ContactUsVM src);
    }
}
