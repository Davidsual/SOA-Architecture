using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Dispatcher;
using Autofac;
using DavideTrotta.Wcf.Contract;


namespace DavideTrotta.Wcf.MessageExchange.Host
{
    public class MessageExchangeRunner
    {
        private ServiceHost _serviceHost;

        public void Start(IContainer container)
        {
            Console.WriteLine("Sample Service Started.");
            //Console.WriteLine("Sample Dependency: {0}", _service);

            if (_serviceHost != null)
                _serviceHost.Close();
            
            _serviceHost = new ServiceHost(container.Resolve<IMessageExchangeService>());

            var exceptionHandler = new WcfExceptionHandler();
            
            ExceptionHandler.TransportExceptionHandler = exceptionHandler;
            ExceptionHandler.AsynchronousThreadExceptionHandler = exceptionHandler;
            
            _serviceHost.Faulted += OnServiceFaulted;

            _serviceHost.Open();
        }
        private static void OnServiceFaulted(object sender, EventArgs e)
        {
            //Log
        }

        public void Stop(IContainer container)
        {
            if (_serviceHost == null)
                return;

            _serviceHost.Close();
            _serviceHost = null;
            container.Dispose();
        }
    }

    public class WcfExceptionHandler : ExceptionHandler
    {
        public override bool HandleException(Exception exception)
        {
            return true;
        }
    }
}
