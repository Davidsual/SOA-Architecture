using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace DavideTrotta.Wcf.DataProvider.Infrastructure
{
    public class DataProviderModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type =>  type.Name.EndsWith("DataProvider")).AsImplementedInterfaces();
        }
    }
}
