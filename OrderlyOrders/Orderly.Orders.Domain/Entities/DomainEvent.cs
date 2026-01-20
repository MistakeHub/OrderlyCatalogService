using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Domain.Entities
{
    public record DomainEvent : IDomainEvent
    {
        public DateTime OccuredTime { get; set; } = DateTime.Now;
    }
}
