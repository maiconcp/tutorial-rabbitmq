using System;
using System.Text;
using RabbitMQ.Client;

namespace PublisherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            new RpcServer().Run();

            //new PublisherDirectJson().Run();

            //new PublisherDirect().Run(args);

            //new PublisherFunout().Run();
            
            //new WorkerProducer().Run();            
        }
    }
}
