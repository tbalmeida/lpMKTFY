using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        // Error message

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<OrderVM> Create(OrderCreateVM src)
        {
            var entity = new Order(src);
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new OrderVM(entity);
        }

        public Task<OrderVM> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderVM> GetByUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
