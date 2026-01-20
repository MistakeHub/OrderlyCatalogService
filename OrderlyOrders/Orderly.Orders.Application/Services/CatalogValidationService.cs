using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Application.CommandsAndQueries.Order.Create;
using Orderly.Orders.Domain.Entities;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Application.Services
{
    public class CatalogValidationService : ICatalogValidationService
    {
        private readonly ICatalogClient _catalog;

        public CatalogValidationService(ICatalogClient catalog)
        {
            _catalog = catalog;
        }

        public async Task<List<OrderItem>> ValidateAndBuildItemsAsync(
            IEnumerable<CreateOrderItemDto> items)
        {
            var result = new List<OrderItem>();

            foreach (var item in items)
            {

                var product = await _catalog.GetByIdAsync(item.ProductId) ?? throw new InvalidOperationException($"Product {item.ProductId} not found");

                result.Add(new OrderItem { ProductName = product.ProductName, ProductId = item.ProductId, Quantity = item.Quantity, UnitPrice = product.Price });
            }

            return result;
        }
    }

}
