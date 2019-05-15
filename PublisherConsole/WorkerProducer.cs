using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublisherConsole
{
    public class WorkerProducer
    {
        public void Run()
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("= Worker - Producer =======================================");
            Console.WriteLine("===========================================================");
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "task_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                do
                {
                    Console.WriteLine("Entre com a mensagem (cada . equivale a 100ms de processamento): ");
                    string mensagem = Console.ReadLine();
                    channel.BasicPublish(exchange: "",
                         routingKey: "task_queue",
                         basicProperties: properties,
                         body: Encoding.UTF8.GetBytes(mensagem));

                    Console.WriteLine("Mensagem Enviada: " + mensagem);

                }
                while (true);
            }
        }
    }
}
