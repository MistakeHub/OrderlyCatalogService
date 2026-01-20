using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Orders.Domain.Interfaces
{
    public interface IDomainEvent
    {
        DateTime OccuredTime { get; set; }
    }
}
