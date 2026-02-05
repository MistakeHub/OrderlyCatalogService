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
        private IDomainEventDispatcher _domainEventDispatcher;

        public OrderRepository(OrderlyOrderDbContext context, IDomainEventDispatcher domainEventDispatcher)
        {
            _context = context;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<int> AddAsync(Order order)
        {
            var neworder = await  _context.Orders.AddAsync(order);
            
            await SaveChangesAsync();

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

            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            var entities = _context
           .ChangeTracker
           .Entries<Order>()
           .Select(e => e.Entity)
           .ToList();

            var events = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            await _domainEventDispatcher.DispatchAsync(events);

            foreach (var entity in entities)
            {
                entity.ClearDomainEvents();
            }

        }
    }
}
