using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Orders.Domain.Interfaces;

namespace Orderly.Orders.Application.Common.DomainEvent
{
    public class DomainEventDispatcher:IDomainEventDispatcher
    {
        private readonly IMessageBrokerPublishService _messageBrokerPublishService;

        public DomainEventDispatcher(IMessageBrokerPublishService messageBrokerPublishService)
        {
            _messageBrokerPublishService = messageBrokerPublishService;
        }

        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
               await _messageBrokerPublishService.Publish(new Domain.Entities.BrokerMessage<IDomainEvent> { Message = domainEvent, routingKey = domainEvent.GetRoutingKey() });
            }

        }
    }
}
