using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavideTrotta.Wcf.Contract.Command;

namespace DavideTrotta.Wcf.Handlers.Handlers
{
    public interface IUpdateUserHandler : ICommandHandler<UpdateUserCommand>{}

    public class UpdateUserHandler : IUpdateUserHandler
    {
        public void Handle(UpdateUserCommand command)
        {
            //throw new NotImplementedException();
        }
    }
}
