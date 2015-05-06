using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using DavideTrotta.Wcf.Common.Exceptions;
using DavideTrotta.Wcf.Contract.Exception;


namespace DavideTrotta.Wcf.Service.Infrastructure
{
    public class GlobalErrorHandler : IErrorHandler, IServiceBehavior
    {
        public void AddBindingParameters(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler = new GlobalErrorHandler();

            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;

                if (channelDispatcher != null)
                {
                    channelDispatcher.ErrorHandlers.Add(errorHandler);
                }
            }
        }

        public bool HandleError(Exception error)
        {
            Trace.TraceError(error.ToString());

            // Returning true indicates you performed your behavior.
            return true;
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            FaultException faultException;


            if (error != null && error is ObjectValidationException)
            {
                faultException = new FaultException<ValidationFault>(new ValidationFault(((ObjectValidationException)error).ValidationErrors.Select(item =>
                    new DavideTrotta.Wcf.Contract.Exception.ValidationError
                    {
                        ErrorMessage = item.ErrorMessage,
                        ObjectName = item.ObjectName,
                        PropertyName = item.PropertyName
                    })), new FaultReason("Validation errro"));
            }
            else
            {
                faultException = new FaultException(
                string.Format("Exception caught at GlobalErrorHandler{0}Method: {1}{2}Message:{3}",
                             Environment.NewLine, error.TargetSite.Name, Environment.NewLine, error.Message));
            }
            // Shield the unknown exception
            //FaultException faultException = new FaultException<SimpleErrorFault>(new SimpleErrorFault(), new FaultReason("Server error encountered. All details have been logged."));
                //new FaultException("Server error encountered. All details have been logged.");
            MessageFault messageFault = faultException.CreateMessageFault();

            fault = Message.CreateMessage(version, messageFault, faultException.Action);
        }
    }
}
