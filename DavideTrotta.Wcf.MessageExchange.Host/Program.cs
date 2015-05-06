﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.NLog;
using Autofac.Features.Variance;
using DavideTrotta.Wcf.Contract;
using DavideTrotta.Wcf.DataProvider.Infrastructure;
using DavideTrotta.Wcf.Handlers.Infrastructure;
using DavideTrotta.Wcf.MessageExchange.Host.Implementation;
using Topshelf;

namespace DavideTrotta.Wcf.MessageExchange.Host
{
    class Program
    {
        static void Main()
        {

            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterModule<NLogModule>();
            builder.RegisterType<MessageExchangeService>().As<IMessageExchangeService>().SingleInstance();
            builder.RegisterType<MessageExchangeRunner>();
            builder.RegisterModule<HandlerModule>();
            builder.RegisterModule<NLogModule>();
            builder.RegisterModule<DataProviderModule>();

            var container = builder.Build();

            HostFactory.Run(c =>
            {
                c.RunAsLocalSystem();
                // Pass it to Topshelf
                c.Service<MessageExchangeRunner>(s =>
                {
                    s.ConstructUsing(name => container.Resolve<MessageExchangeRunner>());
                    s.WhenStarted(service => service.Start(container));
                    s.WhenStopped(service => service.Stop(container));                    
                });
                c.SetDescription("Handles DataCash card payments");
                c.SetDisplayName("Moneycorp Kerching Payment Service");
                c.SetServiceName("MoneycorpKerchingService");

                c.EnableServiceRecovery(a => a.RestartService(1));

                c.UseNLog();
            });

            Console.ReadLine();
        }
    }
}
