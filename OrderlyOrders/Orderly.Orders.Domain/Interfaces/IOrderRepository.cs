using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Domain.Entities;

namespace Orderly.Orders.Domain.Interfaces
{
    public interface IOrderRepository
    {
        public Task<int> AddAsync(Orderly.Orders.Domain.Entities.Order order);

        public Task UpdateAsync(Orderly.Orders.Domain.Entities.Order order);

        public Task<Orderly.Orders.Domain.Entities.Order> GetByIdAsync(int orderId);

        public Task<List<Order>> GetAllAsync();
    }
}
