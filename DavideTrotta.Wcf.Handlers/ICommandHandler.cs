using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavideTrotta.Wcf.Contract.Command;

namespace DavideTrotta.Wcf.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface ICommandHandlerAsync<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
