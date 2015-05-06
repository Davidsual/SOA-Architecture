using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using DavideTrotta.Wcf.Contract;
using DavideTrotta.Wcf.Contract.Command;
using DavideTrotta.Wcf.Handlers.Handlers;

namespace DavideTrotta.Wcf.Handlers.Infrastructure
{
    public class HandlerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CacheProvider>().As<ICacheProvider>().SingleInstance();

            builder.RegisterAssemblyTypes(ThisAssembly).AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces().InstancePerLifetimeScope();            
            builder.RegisterAssemblyTypes(ThisAssembly)
            .As(t => t.GetInterfaces()
            .Where(a => a.IsClosedTypeOf(typeof(IQueryHandler<,>)))
            .Select(a => new KeyedService("queryHandler", a)));

            builder.RegisterGenericDecorator(
                typeof(CacheQueryDecorator<,>),
                typeof(IQueryHandler<,>),
                fromKey: "queryHandler");

            builder.RegisterType<Bus>().As<IBus>();
        }
    }
}
