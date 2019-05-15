using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsumerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            new RpcClient().Run(args);

            // new SubscribeDirectJson().Run();

            // new SubscribeDirect().Run(args);

            //new SubscriberFanout().Run();

            // new WorkerConsumer().Run();
        }
    }
}
