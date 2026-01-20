using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.Create
{
    public class CreateOrderItemDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
