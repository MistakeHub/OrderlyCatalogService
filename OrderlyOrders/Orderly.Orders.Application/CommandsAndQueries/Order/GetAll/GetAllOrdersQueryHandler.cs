using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orderly.Orders.Application.CommandsAndQueries.Order.Create;
using Orderly.Orders.Application.CommandsAndQueries.ViewModels;
using Orderly.Orders.Domain.Entities;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.GetAll
{
    public class GetAllOrdersQueryHandler:IRequestHandler<GetAllOrders, List<OrderListViewModel>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;


        public GetAllOrdersQueryHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderListViewModel>> Handle(GetAllOrders request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderListViewModel>>(orders);
        }
    }
}
