using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Orders.Application.CommandsAndQueries.ViewModels;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.GetById
{
    public class GetByIdOrder:IRequest<OrderViewModel>
    {
        public int OrderId { get; set; }
    }
}
