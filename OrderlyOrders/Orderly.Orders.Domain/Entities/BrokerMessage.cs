using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Orders.Domain.Entities
{
    public class BrokerMessage<T>
    {

        public string routingKey { get; set; }

        public T Message { get; set; }
    }
}
