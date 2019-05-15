using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerConsole
{
    public class WorkerConsumer
    {
        public void Run()
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("= Worker - Consumer =======================================");
            Console.WriteLine("===========================================================");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "task_queue",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        JobExecute(message);

                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };

                    channel.BasicConsume(queue: "task_queue", autoAck: false, consumer: consumer);

                    Console.WriteLine("Waiting for a job...");
                    do
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    while (true);
                }
            }
        }

        private void JobExecute(string message)
        {
            Console.WriteLine(message);

            int dots = message.Split('.').Length - 1;

            Console.Write("".PadLeft(dots, '°'));
            Console.SetCursorPosition(0, Console.CursorTop);

            for (int i = 0; i < dots; i++)
            {
                Console.Write('.');
                System.Threading.Thread.Sleep(1000);
            }
            Console.WriteLine();
            Console.WriteLine("Job Complete!");
        }
    }
}
