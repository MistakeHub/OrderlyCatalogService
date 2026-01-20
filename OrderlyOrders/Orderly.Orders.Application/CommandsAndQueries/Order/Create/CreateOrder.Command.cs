using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.Create
{
    public class CreateOrder:IRequest<int>
    {
        public int CustomerId { get; set; }

        public List<CreateOrderItemDto> Items { get; set; }
    }
}
