using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublisherConsole
{
    public class PublisherFunout
    {
        public void Run()
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("= Publisher - fanout ======================================");
            Console.WriteLine("===========================================================");
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                do
                {
                    Console.WriteLine("Entre com a mensagem a ser enviada: ");
                    string mensagem = Console.ReadLine();
                    channel.BasicPublish(exchange: "logs",
                         routingKey: "",
                         basicProperties: null,
                         body: Encoding.UTF8.GetBytes(mensagem));

                    Console.WriteLine("Mensagem Enviada: " + mensagem);

                }
                while (true);
            }
        }
    }
}
