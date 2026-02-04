using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Orderly.Orders.Domain.Entities;
using Orderly.Orders.Domain.Interfaces;
using RabbitMQ.Client;

namespace Orderly.Orders.Infrastructure.Implements
{
    public class RabbitMQPublisher : IMessageBrokerPublishService
    {
        private IRabbitMqConnectionProvider _connectionProvider;
        private readonly RabbitMqOptions _options;
        public RabbitMQPublisher(IRabbitMqConnectionProvider connectionProvider, RabbitMqOptions options ) { _connectionProvider = connectionProvider; _options = options; }
        public async Task Publish<T>(BrokerMessage<T> message)
        {
            var connection =await _connectionProvider.GetConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: _options.Exchange,
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false);
           
            var json = JsonSerializer.Serialize(message.Message);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = new BasicProperties
            {
                Persistent = true,
                ContentType = "application/json",
                ContentEncoding = "urf-8"
            };

            await channel.BasicPublishAsync(exchange: _options.Exchange, routingKey: message.routingKey, basicProperties: properties, body: body, mandatory:false);
        }
    }
}
