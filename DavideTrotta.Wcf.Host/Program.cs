using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Autofac;
using Autofac.Extras.NLog;
using Autofac.Features.Variance;
using DavideTrotta.Wcf.Contract;
using DavideTrotta.Wcf.Contract.MessageExchange.Request;
using DavideTrotta.Wcf.DataProvider.Infrastructure;
using DavideTrotta.Wcf.Handlers.Infrastructure;
using DavideTrotta.Wcf.Service;
using NLog;
using Topshelf;


namespace DavideTrotta.Wcf.Host
{
    class Program
    {

        static void Main()
        {

            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterModule<HandlerModule>();
            builder.RegisterModule<NLogModule>();
            builder.RegisterModule<DataProviderModule>();
            builder.RegisterType<MyService>().As<IMyService>().SingleInstance();
            builder.RegisterType<Service>();

            var container = builder.Build();


            SendMessageToMsMQ();

            HostFactory.Run(c =>
            {
                c.RunAsLocalSystem();
                // Pass it to Topshelf
                c.Service<Service>(s =>
                {
                    s.ConstructUsing(name => container.Resolve<Service>());
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

        private static void SendMessageToMsMQ()
        {            
            NetMsmqBinding binding = new NetMsmqBinding();
            EndpointAddress address = new EndpointAddress(@"net.msmq://localhost/private/TestQueue");
            ChannelFactory<IMessageExchangeService> channelFactory = new ChannelFactory<IMessageExchangeService>(binding, address);
            IMessageExchangeService channel = channelFactory.CreateChannel();

            GetContactRequest po = new GetContactRequest();
            po.RequestId = new Guid();
            po.Id = 123;


            //MsmqMessage<GetContactRequest> ordermsg = new MsmqMessage<GetContactRequest>(po);
            //ordermsg.Label = po.GetType().Name;
            for (int i = 0; i < 100; i++)
            {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {

                channel.GetContact(po);
                    scope.Complete();
               

            }
            }
            Console.WriteLine("Order has been submitted:{0}", po);

        }
    }
}
