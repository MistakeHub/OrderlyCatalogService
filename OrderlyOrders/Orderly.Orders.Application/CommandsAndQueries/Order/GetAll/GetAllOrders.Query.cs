using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Orders.Application.CommandsAndQueries.ViewModels;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.GetAll
{
    public class GetAllOrders:IRequest<List<OrderListViewModel>>
    {
    }
}
