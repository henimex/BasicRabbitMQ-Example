using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;

namespace RabbitMQSend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string QueName = "Henimex";
            string RabbitServer = "";
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(RabbitServer);

            using var connection = factory.CreateConnection();

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: QueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                string message = "Hello World of RabbitMQ";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: QueName,
                    basicProperties: null,
                    body: body
                    );

                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine("Waitng for exit");
            Console.ReadLine();
        }
    }
}
