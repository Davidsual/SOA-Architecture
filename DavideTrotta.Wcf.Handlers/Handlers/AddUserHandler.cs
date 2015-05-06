using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavideTrotta.Wcf.Contract.Command;
using DavideTrotta.Wcf.Contract.Entities;
using DavideTrotta.Wcf.DataProvider;

namespace DavideTrotta.Wcf.Handlers.Handlers
{
    public interface IAddUserHandler : ICommandHandler<CreateUserCommand> { }
    public class AddUserHandler : IAddUserHandler
    {
        private readonly IContactDataProvider _contactDataProvider;

        public AddUserHandler(IContactDataProvider contactDataProvider)
        {
            _contactDataProvider = contactDataProvider;
        }
        public void Handle(CreateUserCommand command)
        {
            int contactId = _contactDataProvider.CreateContact(new Contact() { Lastname = command.LastName, Name = command.Name });

            if (contactId % 2 == 0)
                throw new Exception("Some silly exception for checking the transactions");

            Console.WriteLine("SAved: "+contactId);
            _contactDataProvider.CreateContactDetail(new Contact()
            {
                Lastname = command.LastName,
                Name = command.Name,
                Id = contactId
            });
        }
    }
}
