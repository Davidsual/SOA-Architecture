using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DavideTrotta.Wcf.Common.Exceptions;
using DavideTrotta.Wcf.Contract.Command;
using DavideTrotta.Wcf.Contract.Query;

namespace DavideTrotta.Wcf.Handlers
{

    public interface IBus
    {
        void Execute<TCommand>(TCommand command) where TCommand : Command;
        TResult Dispatch<TParameter, TResult>(TParameter query) where TParameter : Query<TResult>;
    }



    public class Bus : IBus
    {
        private readonly IComponentContext _container;

        public Bus(IComponentContext container)
        {
            if (container == null) 
                throw new ArgumentNullException("container");

            _container = container;
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query) where TParameter : Query<TResult>
        {
            if (query == null) throw new ArgumentNullException("query");

            ICollection<ValidationResult> validationResults;
            if (!query.IsValid(out validationResults))
            {
                throw new ObjectValidationException(validationResults.Select(item => new DavideTrotta.Wcf.Common.Exceptions.ValidationError
                {
                    ErrorMessage = item.ErrorMessage,
                    ObjectName = query.GetType().Name,
                    PropertyName = item.MemberNames.FirstOrDefault()
                }));
            }
                
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            var handler = _container.Resolve<IQueryHandler<TParameter, TResult>>();

            if (Object.ReferenceEquals(null, handler))
                throw new InfrastructureException(string.Format("Query handler not found for input query: {0}", handlerType.Name));


            var a = System.Attribute.GetCustomAttributes(handler.GetType());

            return handler.Handle(query);
        }

        public void Execute<TCommand>(TCommand command) where TCommand : Command
        {

            if (command == null) throw new ArgumentNullException("command");

            ICollection<ValidationResult> validationResults;

            if (!command.IsValid(out validationResults))
            {
                throw new ObjectValidationException(validationResults.Select(item => new DavideTrotta.Wcf.Common.Exceptions.ValidationError
                {
                    ErrorMessage = item.ErrorMessage,
                    ObjectName = command.GetType().Name,
                    PropertyName = item.MemberNames.FirstOrDefault()
                }));
            }

            var handler = _container.Resolve<ICommandHandler<TCommand>>();

            if (Object.ReferenceEquals(null, handler))
                throw new InfrastructureException(string.Format("Command handler not found for input query: {0}", typeof(ICommandHandler<TCommand>).Name));

            handler.Handle(command);

        }
    }
}
