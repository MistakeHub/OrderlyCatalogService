using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.Cancel
{
    internal class CancelOrderCommandHandler : IRequestHandler<CancelOrder>
    {
        private readonly ICatalogClient _catalogClient;
        private readonly IOrderRepository _orderRepository;

        public CancelOrderCommandHandler(ICatalogClient catalogClient, IOrderRepository orderRepository)
        {
            _catalogClient = catalogClient;
            _orderRepository = orderRepository;
        }
            
        public async Task Handle(CancelOrder request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId) ?? throw new InvalidOperationException($"Order {request.OrderId} not found"); ;

            order.Cancel();

           await _orderRepository.UpdateAsync(order);

        }
    }
}
