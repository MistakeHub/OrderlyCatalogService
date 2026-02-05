using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Domain.Entities;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Domain.Domain_Events
{
    public record OrderCreated(int OrderId, int CustomerId, decimal Total) : DomainEvent
    {
        public override string GetRoutingKey() => "order.created";
    }
    public record OrderCancelled(int OrderId) : DomainEvent
    {
        public override string GetRoutingKey() => "order.cancelled";
    }
    public record OrderPaid(int OrderId, decimal Total) : DomainEvent
    {
        public override string GetRoutingKey() => "order.paid";
    }
}
