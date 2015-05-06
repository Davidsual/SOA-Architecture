using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using System.Text;
using System.Threading.Tasks;
using DavideTrotta.Wcf.Contract;
using DavideTrotta.Wcf.Contract.MessageExchange.Request;
using DavideTrotta.Wcf.Handlers;

namespace DavideTrotta.Wcf.MessageExchange.Host.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MessageExchangeService : IMessageExchangeService
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void GetContact(GetContactRequest request)
        {
            Console.WriteLine("I received a contract: {0}", request.Id);
        }
    }
}
