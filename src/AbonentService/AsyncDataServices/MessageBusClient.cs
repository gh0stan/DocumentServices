using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using AbonentService.Dtos;
using RabbitMQ.Client;
using System.Threading.Channels;

namespace AbonentService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(_configuration["RabbitMQAbonentExchange"], 
                        type: ExchangeType.Fanout, 
                        durable: false, 
                        autoDelete: false);
                
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                
                Console.WriteLine("DEBUG. Connected to MessageBus.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR. Could not connect to the MessageBus: {ex.Message}.");
            }
        }

        public void CreateNewAbonent(AbonentCreatedDto abonentCreatedDto)
        {
            var message = JsonSerializer.Serialize(abonentCreatedDto);

            if (_connection.IsOpen)
            {
                Console.WriteLine("DEBUG. RabbitMQ Connection open, sending messages...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("DEBUG. RabbitMQ connection is closed, not sending.");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: _configuration["RabbitMQAbonentExchange"],
                            routingKey: "",
                            basicProperties: null,
                            body: body);
            Console.WriteLine($"DEBUG. Message sent. {message}.");
        }

        public void Dispose()
        {
            Console.WriteLine("DEBUG. MessageBus Disposed.");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("DEBUG. RabbitMQ Connection Shutdown.");
        }
    }
}
