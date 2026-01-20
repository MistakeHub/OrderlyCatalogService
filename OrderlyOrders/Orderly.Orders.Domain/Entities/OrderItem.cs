using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Orders.Domain.Entities
{
    public class OrderItem:BaseEntity
    {

        public int OrderId { get; set; }

        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => UnitPrice * Quantity;

        public Order Order { get; set; }

    }
}
