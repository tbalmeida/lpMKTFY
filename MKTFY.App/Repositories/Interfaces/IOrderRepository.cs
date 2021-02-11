using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> Create(OrderCreateVM src);

        Task<List<OrderVM>> GetByUser(string userId, [Optional] bool? active);

        Task<OrderVM> GetById(Guid id);

        Task<OrderVM> Update(OrderUpdateVM src);
    }
}
