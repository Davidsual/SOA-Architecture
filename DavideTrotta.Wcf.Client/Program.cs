using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DavideTrotta.Wcf.Client.ServerIntergration;
using DavideTrotta.Wcf.Contract.Command;
using Microsoft.AspNet.SignalR.Client;

namespace DavideTrotta.Wcf.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HubConnection hubConnection;
            IHubProxy hubProxy;
            try
            {
                hubConnection = new HubConnection("http://localhost:63891/signalr/hubs");
                hubProxy = hubConnection.CreateHubProxy("MyTesttHub");
                hubProxy.On<string>("newMessageReceived", (message) => Console.WriteLine(message));
                hubConnection.Start().Wait();
                hubProxy.Invoke("BroadcastMessageToAll", "test message from console app").Wait();
                Console.WriteLine("Connected");
            }
            catch (Exception)
            {
                    
                throw;
            }

            
            //

            /*
            var builder = new ContainerBuilder();
            builder.RegisterModule<WcfModule>();
            builder.RegisterType<ServerService>().As<IServerService>();
            Console.Write("Davide");
            var container = builder.Build();
            var res = container.Resolve<IServerService>().CreateUser(new CreateUserCommand());
            Console.WriteLine("Result from server " + res);
             * */
            Console.ReadLine();
        }
    }
}
