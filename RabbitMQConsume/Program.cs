using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQConsume
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

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);

                channel.BasicConsume(
                    queue: QueName,
                    autoAck: true,
                    consumer: consumer);


                consumer.Received += Consumer_Received;

                Console.WriteLine("Enter To Exit");
                Console.ReadLine();
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
        }
    }
}
