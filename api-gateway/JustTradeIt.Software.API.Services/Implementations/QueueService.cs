using System;
using JustTradeIt.Software.API.Services.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class QueueService : IQueueService, IDisposable
    {
        protected IModel Channel { get; private set; }
        private IConnection _connection;

        public void Dispose()
        {
            // TODO: Dispose the connection and channel
            try
            {
                Channel?.Close();
                Channel?.Dispose();
                Channel = null;

                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot dispose RabbitMQ channel or connection");
            }
        }

        public void PublishMessage(string routingKey, object body)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
             if (_connection == null || _connection.IsOpen == false)
            {
                _connection = factory.CreateConnection();
            }
            if (Channel == null || Channel.IsOpen == false)
            {
                Channel = _connection.CreateModel();
                var message = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body));
                var props = Channel.CreateBasicProperties();
                props.ContentType = "application/json";
                props.DeliveryMode = 1;
                Channel.ExchangeDeclare(exchange: "item-exchange", type: "direct", durable: true);
                Channel.BasicPublish(exchange: "item-exchange",
                                    routingKey: routingKey,
                                    basicProperties: props,
                                    body: message);
            }
        }
    }
}