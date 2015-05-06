using DavideTrotta.Wcf.Contract.Command;
using DavideTrotta.Wcf.Contract.Entities;
using DavideTrotta.Wcf.Contract.Exception;
using System.ServiceModel;
using DavideTrotta.Wcf.Contract.Query;

namespace DavideTrotta.Wcf.Contract
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IMyService
    {

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(SimpleErrorFault))]
        void ExecuteCommand(Command.Command command);

        [OperationContract]
        [FaultContract(typeof (ValidationFault))]
        [FaultContract(typeof (SimpleErrorFault))]
        CommandResponse UpdateUser(Contract.Command.UpdateUserCommand updateUser);

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(SimpleErrorFault))]
        CommandResponse CreateUser(CreateUserCommand createUser);

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(SimpleErrorFault))]
        Contact GetContact(GetContactByIdQuery getContactByIdQuery);
    }
}
