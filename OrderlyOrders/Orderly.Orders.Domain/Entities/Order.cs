using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Domain.Domain_Events;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Domain.Entities
{
   public class Order:BaseEntity
    {

        public int CustomerId { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<OrderItem> Items { get; set; } = new();

        [NotMapped]
        private readonly List<IDomainEvent> _events = new();

        [NotMapped]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _events;

        public static Order Create(int customerId, List<OrderItem> items, decimal total)
        {
            if(items == null || items.Count == 0)
            {
                throw new InvalidOperationException("Cannot create order without a single item.");
            }

            if(items.Any(x=> x.Quantity == 0))
            {
                throw new InvalidOperationException("Cannot create order where items quantity is equal 0.");
            }

            if (items.Any(x => x.UnitPrice == 0))
            {
                throw new InvalidOperationException("Cannot create order where items unitPrice is equal 0.");
            }

            var order = new Order
            {
                CustomerId = customerId,
                Items = items,
                Status = OrderStatus.Pending,
                TotalPrice = total
            };

            order.AddEvent(new OrderCreated(order.Id, customerId, order.TotalPrice));

            return order;
        }

        public void Pay()
        {
            if(Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException("Cannot pay order in this state.");
            }

            if(Status == OrderStatus.Paid)
            {
                throw new InvalidOperationException("Order is already paid.");
            }

            Status = OrderStatus.Paid;

            AddEvent(new OrderPaid(Id, TotalPrice));
        }

        public void Cancel()
        {
            if(Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
            {
                throw new InvalidOperationException("Cannot cancel shipped or delivered order");
            }

            Status = OrderStatus.Cancelled;

            AddEvent(new OrderCancelled(Id));
        }

        private void AddEvent(IDomainEvent e)=> _events.Add(e);
        public void ClearDomainEvents()
        {
            _events.Clear();
        }
    }

    public enum OrderStatus:int
    {
        Pending=0,
        Paid= 1,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
