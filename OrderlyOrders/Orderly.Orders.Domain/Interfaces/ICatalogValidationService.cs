using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Application.CommandsAndQueries.Order.Create;
using Orderly.Orders.Domain.Entities;

namespace Orderly.Orders.Domain.Interfaces
{
    public interface ICatalogValidationService
    {
        public Task<List<OrderItem>> ValidateAndBuildItemsAsync(
         IEnumerable<CreateOrderItemDto> items);
   
    }
}
