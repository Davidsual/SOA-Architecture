using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using DavideTrotta.Wcf.Contract.Query;

namespace DavideTrotta.Wcf.Handlers
{
    public interface IQueryHandler<TQuery,TResult> where TQuery : IQuery<TResult> 
    {
        TResult Handle(TQuery query);
    }

    public interface ICachedQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult> 
    {
        TResult Handle(TQuery query); 
    }


    public interface IQueryHandlerAsync<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}
