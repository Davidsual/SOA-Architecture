using DavideTrotta.Wcf.Contract.Query;
using DavideTrotta.Wcf.DataProvider;
using DavideTrotta.Wcf.Handlers.Infrastructure;

namespace DavideTrotta.Wcf.Handlers.Handlers
{
    //public interface IGetUserHandler : IQueryHandler<GetUserQuery,GetUserResult>{}

    public class GetUserHandler : IQueryHandler<GetUserQuery,GetUserResult>
    {
        private readonly IContactDataProvider _contactDataProvider;

        public GetUserHandler(IContactDataProvider contactDataProvider)
        {
            _contactDataProvider = contactDataProvider;
        }

        public GetUserResult Handle(GetUserQuery query)
        {
            return null;
        }
    }
}
