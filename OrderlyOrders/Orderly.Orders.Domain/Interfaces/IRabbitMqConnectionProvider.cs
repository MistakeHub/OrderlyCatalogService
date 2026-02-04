using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Orderly.Orders.Domain.Interfaces
{
    public interface IRabbitMqConnectionProvider
    {
        Task<IConnection> GetConnectionAsync();
    }
}
