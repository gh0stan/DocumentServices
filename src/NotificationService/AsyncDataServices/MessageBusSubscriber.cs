using MongoDB.Driver.Core.Connections;
using NotificationService.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using static MongoDB.Driver.WriteConcern;
using IConnection = RabbitMQ.Client.IConnection;

namespace NotificationService.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
        {
            private readonly IConfiguration _configuration;
            private readonly IEventProcessor _eventProcessor;
            private IConnection _connection;
            private IModel _channel;
            private string _queueName;

            public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
            {
                _configuration = configuration;
                _eventProcessor = eventProcessor;

                try
                {
                    InitializeRabbitMQ();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR. Unable to initialize RabbitMQ. Host: {_configuration["RabbitMQHost"]}. Port: {_configuration["RabbitMQPort"]}. {ex.Message}");
                    throw;
                }
            }

            private void InitializeRabbitMQ()
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _configuration["RabbitMQHost"],
                    Port = int.Parse(_configuration["RabbitMQPort"])
                };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: _configuration["RabbitMQNotificationExchange"], type: ExchangeType.Fanout);
                _queueName = _channel.QueueDeclare().QueueName;

                _channel.QueueBind(queue: _queueName,
                    exchange: _configuration["RabbitMQNotificationExchange"],
                    routingKey: "");

                Console.WriteLine("DEBUG. Listening on the MessageBus...");

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            }

            protected override Task ExecuteAsync(CancellationToken stoppingToken)
            {
                stoppingToken.ThrowIfCancellationRequested();
                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (model, ea) =>
                {
                    Console.WriteLine("DEBUG. Event received.");

                    var body = ea.Body;
                    var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                    _eventProcessor.ProcessEvent(notificationMessage);
                };

                _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

                return Task.CompletedTask;
            }

            private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
            {
                Console.WriteLine("DEBUG. RabbitMQ connection shutdown.");
            }

            public override void Dispose()
            {
                if (_channel.IsOpen)
                {
                    _channel.Close();
                    _connection.Close();
                }

                base.Dispose();
            }
        }
}
