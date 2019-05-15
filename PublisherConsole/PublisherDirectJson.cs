using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublisherConsole
{
    public class PublisherDirectJson
    {
        public void Run()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_json",
                                        type: "direct");

                var message = ObterEvento();
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "direct_json",
                                     routingKey: nameof(PessoaCriadaEvent),
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent '{0}':'{1}'", nameof(PessoaCriadaEvent), message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private string ObterEvento()
        {
            var pessoaCriadaEvent = new PessoaCriadaEvent()
            {
                Id = 15,
                Nome = "Maicon",
                Cnpj = "11111"
            };

            return JsonConvert.SerializeObject(pessoaCriadaEvent);
        }
    }


    public class PessoaCriadaEvent
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
    }
}
