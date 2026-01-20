using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orderly.Orders.Application.CommandsAndQueries.Order.GetById;
using Orderly.Orders.Application.CommandsAndQueries.ViewModels;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.Update
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrder>
    {
        private readonly IOrderRepository _orderRepository;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(UpdateOrder request, CancellationToken cancellationToken)
        {
           await _orderRepository.UpdateAsync(new Orderly.Orders.Domain.Entities.Order { Id = request.Id, Status = request.Status, CustomerId = request.CustomerId, Items = request.Items });
        }
    }
}
