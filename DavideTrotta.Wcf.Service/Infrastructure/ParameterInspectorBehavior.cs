using System;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace DavideTrotta.Wcf.Service.Infrastructure
{
    internal class ParameterInspectorBehavior : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            var validationInspector = new ValidatingParameterInspector();
            var operations =
                    from dispatcher in serviceHostBase.ChannelDispatchers.Cast<ChannelDispatcher>()
                    from endpoint in dispatcher.Endpoints
                    from operation in endpoint.DispatchRuntime.Operations
                    select operation;

            operations.ToList().ForEach(operation => operation.ParameterInspectors.Add(validationInspector));
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            
        }
    }
}
