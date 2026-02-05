using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Orderly.Orders.Domain.Interfaces;
using RabbitMQ.Client;

namespace Orderly.Orders.Infrastructure.Implements
{
    public class RabbitMqConnectionProvider:IRabbitMqConnectionProvider, IAsyncDisposable
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public RabbitMqConnectionProvider(IConfiguration config)
        {
            _factory = new ConnectionFactory
            {
                Uri = new Uri(config["RabbitMq:Uri"]!),
            };
        }

        public async Task<IConnection> GetConnectionAsync()
        {

            await _lock.WaitAsync();
            try
            {
                if (_connection is not null)
                {
                    if (_connection.IsOpen)
                        return _connection;
                }

                _connection = await _factory.CreateConnectionAsync();
                return _connection;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection is not null)
                await _connection.CloseAsync();
        }
    }
}
