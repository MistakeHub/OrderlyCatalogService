using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Orders.Domain.Entities;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.Create
{
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrder, int>
    {
        private readonly ICatalogValidationService _catalogService;
        private readonly IOrderRepository _orderRepository;
        private readonly IPriceCalculator _priceCalculator;

        public CreateOrderCommandHandler(ICatalogValidationService catalogService, IOrderRepository orderRepository, IPriceCalculator priceCalculator)
        {
            _catalogService = catalogService;
            _orderRepository = orderRepository;
            _priceCalculator = priceCalculator;
        }

        public async Task<int> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var items = await _catalogService.ValidateAndBuildItemsAsync(request.Items);

            decimal total =_priceCalculator.CalculateTotal(items);

            var order = Domain.Entities.Order.Create(request.CustomerId, items, total);

            int orderId = await _orderRepository.AddAsync(order);

            return orderId;
        }
    }
}
