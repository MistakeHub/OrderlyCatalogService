using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Orderly.Orders.Application.CommandsAndQueries.ViewModels;
using Orderly.Orders.Domain.Entities;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.Update
{
    public class UpdateOrder:IRequest
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        
        public decimal TotalPrice => Items.Sum(x => x.TotalPrice);

        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
