using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using DavideTrotta.Wcf.Contract;

namespace DavideTrotta.Wcf.Client.ServerIntergration
{
    public class WcfModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var netTcpBinding = new NetTcpBinding
            {
                CloseTimeout = new TimeSpan(0, 0, 59, 0),
                ReceiveTimeout = new TimeSpan(0, 0, 59, 0),
                SendTimeout = new TimeSpan(0, 0, 59, 0),
                MaxBufferSize = int.MaxValue,
                MaxBufferPoolSize = long.MaxValue,
                Security = new NetTcpSecurity() { Mode = SecurityMode.None }
            };

            builder.Register(c => new ChannelFactory<IMyService>(netTcpBinding, new EndpointAddress(ConfigurationManager.AppSettings["DavideTrottaServerUrl"])))
                    .SingleInstance();

            builder.Register(c => c.Resolve<ChannelFactory<IMyService>>().CreateChannel())
                .As<IMyService>() 
                .UseWcfSafeRelease();

        }
    }
}
