using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MKTFY.App.Exceptions;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public OrderRepository(ApplicationDbContext dbContext, IConfiguration config)
        {
            _context = dbContext;
            _config = config;
        }

        public async Task<Order> Create(OrderCreateVM src)
        {
            var entity = new Order(src);
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();

            var apiKey = _config.GetSection("SGSettings").GetValue<string>("SendGrid");


            return entity;
        }

        public async Task<OrderVM> GetById(Guid id)
        {
            var result = await _context.Orders
                .Include(order => order.Listing)
                .Include(order => order.Listing.Category)
                .Include(order => order.Listing.ItemCondition)
                .Include(order => order.Listing.User)
                .Include(order => order.OrderStatus)
                .FirstOrDefaultAsync(order => order.Id == id);
            if (result == null)
                throw new NotFoundException("Order not found", id.ToString());

            return new OrderVM(result);
        }

        public async Task<List<OrderVM>> GetByUser(string userId, [Optional] bool? active)
        {
            var query = _context.Orders
                .Include(order => order.Listing)
                .Include(order => order.Listing.Category)
                .Include(order => order.Listing.ItemCondition)
                .Include(order => order.Listing.User)
                .Include(order => order.OrderStatus)
                .AsQueryable();

            if (active != null)
                query = query.Where(order => order.Listing.ListingStatus.IsActive == active);

            query = query.Where(order => order.BuyerId == userId);

            var results = await query.OrderBy(lst => lst.Created).ToListAsync();

            return results.ConvertAll(order => new OrderVM(order));
        }

        public async Task<OrderVM> Update(OrderUpdateVM src)
        {
            var current = await _context.Orders.FindAsync(src.Id);
            if (current == null)
                throw new NotFoundException("The order could not be found", src.Id.ToString());

            current.OrderStatusId = src.OrderStatusId;
            current.BuyerId = src.BuyerId;

            await _context.SaveChangesAsync();

            return new OrderVM(current);
        }
    }
}
