using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Domain.Entities;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Application.Services
{
    public class PriceCalculator : IPriceCalculator
    {
        public decimal CalculateTotal(IEnumerable<OrderItem> items)
        {
            return items.Sum(x=> x.Quantity * x.UnitPrice);
        }
    }
}
