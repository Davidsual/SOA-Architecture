using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DavideTrotta.Wcf.Client.ServerIntergration;
using DavideTrotta.Wcf.Contract.Command;

namespace DavideTrotta.Wcf.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<WcfModule>();
            builder.RegisterType<ServerService>().As<IServerService>();
            Console.Write("Davide");
            var container = builder.Build();
            var res = container.Resolve<IServerService>().CreateUser(new CreateUserCommand());
            Console.WriteLine("Result from server " + res);
            Console.ReadLine();
        }
    }
}
