using System.Linq;
using DavideTrotta.Wcf.Contract.Query;
using DavideTrotta.Wcf.Handlers.Infrastructure;

namespace DavideTrotta.Wcf.Handlers.Handlers
{
    public class CacheQueryDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult> 
    {
        private readonly IQueryHandler<TQuery, TResult> _decorator;
        private readonly ICacheProvider _cacheProvider;


        public CacheQueryDecorator(IQueryHandler<TQuery, TResult> decorator,ICacheProvider cacheProvider)
        {
            _decorator = decorator;
            _cacheProvider = cacheProvider;
        }

        public TResult Handle(TQuery query)
        {
            if (!IsCachedQueryHandler())
                return _decorator.Handle(query);

            var cachedResult = _cacheProvider.Get<TResult>(query.GetType().Name);

            if (cachedResult == null)
            {
                cachedResult = _decorator.Handle(query);
                _cacheProvider.Put(query.GetType().Name, cachedResult);
            }

            return cachedResult;
        }

        private bool IsCachedQueryHandler()
        {
            return _decorator.GetType().GetInterfaces()
           .Where(t => t.IsGenericType)
           .Select(t => t.GetGenericTypeDefinition())
           .Any(t => t.Equals(typeof(ICachedQueryHandler<,>)));   
        }
    }
}
