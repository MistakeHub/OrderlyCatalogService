using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orderly.Orders.Application.CommandsAndQueries.Order.GetAll;
using Orderly.Orders.Application.CommandsAndQueries.ViewModels;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.GetById
{
    internal class GetByIdOrderQueryHandler:IRequestHandler<GetByIdOrder, OrderViewModel>
    {
        
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public GetByIdOrderQueryHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<OrderViewModel> Handle(GetByIdOrder request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            return _mapper.Map<OrderViewModel>(order);
           
        }
    }
}
