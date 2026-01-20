using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Domain.Entities;

namespace Orderly.Orders.Application.CommandsAndQueries.ViewModels
{
    public class OrderListViewModel
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
