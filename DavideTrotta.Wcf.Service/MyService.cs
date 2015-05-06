using System.ServiceModel;
using System.Transactions;
using Autofac;
using Autofac.Extras.NLog;
using DavideTrotta.Wcf.Contract;
using DavideTrotta.Wcf.Contract.Command;
using DavideTrotta.Wcf.Contract.Entities;
using DavideTrotta.Wcf.Contract.Query;
using DavideTrotta.Wcf.Handlers;
using DavideTrotta.Wcf.Handlers.Handlers;
using DavideTrotta.Wcf.Service.Infrastructure;

namespace DavideTrotta.Wcf.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Single,
    TransactionIsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
    ReleaseServiceInstanceOnTransactionComplete = false,
    TransactionAutoCompleteOnSessionClose = false)]
    [ParameterInspectorBehavior]
    public class MyService : IMyService
    {
        private readonly ILogger _logger;

        private readonly IUpdateUserHandler _updateUserHandler;
        private readonly IAddUserHandler _addUserHandler;

        private readonly IBus _bus;
        private readonly IComponentContext _container;

        public MyService(ILogger logger, IBus dispatcher, IComponentContext container, IUpdateUserHandler updateUserHandler, IAddUserHandler addUserHandler)
        {
            _logger = logger;
            //_updateUserHandler = updateUserHandler;

            _bus = dispatcher;
            _container = container;
            _updateUserHandler = updateUserHandler;
            _addUserHandler = addUserHandler;
        }

        //[ParameterInspectorBehavior]
        //houndreds of commands hit this endpoint... is it the correct way
        public void ExecuteCommand(Command command)
        {
            var a = _container.Resolve(typeof(ICommandHandler<>));
            //_dispatcher.Execute(command);
            _logger.Info("Test {0}", command.GetType().Name);
        }
        //[OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = false)]
        public Contract.Command.CommandResponse CreateUser(Contract.Command.CreateUserCommand createUser)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted}))
            //{
            _logger.Info("Test {0}", "test");

            //_addUserHandler.Handle(createUser);

            _bus.Execute<CreateUserCommand>(createUser);
            // scope.Complete();
            return new CommandResponse { Id = 1 };
            //}

        }
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public Contract.Command.CommandResponse UpdateUser(Contract.Command.UpdateUserCommand updateUser)
        {
            //_updateUserHandler.Handle(updateUser);
            _bus.Execute<UpdateUserCommand>(updateUser);

            return new CommandResponse { Id = 1 };
        }

        public Contact GetContact(GetContactByIdQuery getContactByIdQuery)
        {
            return _bus.Dispatch<GetContactByIdQuery, Contact>(getContactByIdQuery);
        }
    }
}
