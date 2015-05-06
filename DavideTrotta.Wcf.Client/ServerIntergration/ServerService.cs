using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DavideTrotta.Wcf.Contract;
using DavideTrotta.Wcf.Contract.Command;
using DavideTrotta.Wcf.Contract.Exception;
using DavideTrotta.Wcf.Contract.Query;

namespace DavideTrotta.Wcf.Client.ServerIntergration
{
    public interface IServerService
    {
        int CreateUser(CreateUserCommand createUserCommand);

    }

    public class ServerService : IServerService
    {
        private readonly IMyService _myService;

        public ServerService(IMyService myService)
        {
            _myService = myService;
        }

        public int CreateUser(CreateUserCommand createUserCommand)
        {
            try
            {
                createUserCommand.DateOfBirth = DateTime.Now;
                createUserCommand.Name = "test";
                createUserCommand.LastName = "Dasvsdas 3123 123 12asdasdasdasdasdasd";
                
                var contact = _myService.GetContact(new GetContactByIdQuery() {Id = 2, AdditionalParameter = "dasd"});
                contact = _myService.GetContact(new GetContactByIdQuery() { Id = 2, AdditionalParameter = "dasd" });
                /*
                _myService.UpdateUser(new UpdateUserCommand()
                {
                    DateOfBirth = DateTime.Now,
                    Id = 1123,
                    LastName = "dasdasdasdasdasdasdasd",
                    Name = "da"
                });




                Parallel.For(0, 100, i =>
                {
                    try
                    {
                        createUserCommand.Name = i + " dasvide";
                        _myService.CreateUser(createUserCommand);
                    }
                    catch
                    {
                        
                    }
                });
                */
                return 1;
                //return _myService.CreateUser(createUserCommand).Id;
            }
            catch (FaultException<SimpleErrorFault> sex)
            {
                throw;
            }
            catch (FaultException<ValidationFault> cf)
            {
                throw;
            }
            catch (FaultException fe)
            {
                throw;
            }
        }
    }
}
