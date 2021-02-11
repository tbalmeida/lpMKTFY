using Microsoft.EntityFrameworkCore;
using MKTFY.App.Exceptions;
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

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Order> Create(OrderCreateVM src)
        {
            var entity = new Order(src);
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<OrderVM> GetById(Guid id)
        {
            var result = await _context.Orders
                .Include(order => order.Listing)
                .Include(order => order.Listing.Category)
                .Include(order => order.Listing.ItemCondition)
                .Include(order => order.Listing.User)
                .FirstOrDefaultAsync(order => order.Id == id);
            if (result == null)
                throw new NotFoundException("Order not found", id.ToString());

            return new OrderVM(result);
        }

        public Task<List<OrderVM>> GetByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(OrderUpdateVM src)
        {
            throw new NotImplementedException();
        }
    }
}
