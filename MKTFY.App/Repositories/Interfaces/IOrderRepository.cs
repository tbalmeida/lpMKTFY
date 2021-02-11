using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> Create(OrderCreateVM src);

        Task<List<OrderVM>> GetByUser(string userId);

        Task<OrderVM> GetById(Guid id);

        Task<bool> Update(OrderUpdateVM src);
    }
}
