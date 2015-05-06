using System.ServiceModel.MsmqIntegration;
using DavideTrotta.Wcf.Contract.MessageExchange.Request;
using System.ServiceModel;

namespace DavideTrotta.Wcf.Contract
{
    [ServiceContract(Namespace = "http://DavideTrotta.Wcf.Contract.ServiceContracts", SessionMode = SessionMode.NotAllowed)]
    [ServiceKnownType(typeof(GetContactRequest))]
    public interface IMessageExchangeService
    {
        [OperationContract(IsOneWay = true, Action = "http://DavideTrotta.Wcf.Contract.ServiceContracts")]
        void GetContact(GetContactRequest request);
    }
}
