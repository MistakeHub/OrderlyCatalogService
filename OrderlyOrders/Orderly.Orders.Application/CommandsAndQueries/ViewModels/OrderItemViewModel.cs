using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Orders.Application.CommandsAndQueries.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
