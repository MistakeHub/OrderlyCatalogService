using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Domain.Entities;

namespace Orderly.Orders.Domain.Interfaces
{
    public interface IPriceCalculator
    {
        decimal CalculateTotal(IEnumerable<OrderItem> items);
    }
}
