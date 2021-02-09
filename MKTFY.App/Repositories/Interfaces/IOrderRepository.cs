using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderVM> Create(OrderCreateVM src);

        Task<OrderVM> GetByUser(string userId);

        Task<OrderVM> GetById(Guid id);
    }
}
