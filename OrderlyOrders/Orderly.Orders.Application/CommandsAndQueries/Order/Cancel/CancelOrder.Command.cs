using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.Cancel
{
    public class CancelOrder:IRequest
    {
        public int OrderId { get; set; }
    }
}
