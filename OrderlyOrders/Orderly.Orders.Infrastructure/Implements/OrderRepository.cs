using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orderly.Orders.Domain.Entities;
using Orderly.Orders.Domain.Interfaces;
using Orderly.Orders.Infrastructure.Database;

namespace Orderly.Orders.Infrastructure.Implements
{
    public class OrderRepository:IOrderRepository
    {
        private OrderlyOrderDbContext _context;

        public OrderRepository(OrderlyOrderDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Order order)
        {
            var neworder = await  _context.Orders.AddAsync(order);
            
            await _context.SaveChangesAsync();

            return neworder.Entity.Id;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);   
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);

            await _context.SaveChangesAsync();
        }
    }
}
