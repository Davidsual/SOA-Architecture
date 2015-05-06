using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DavideTrotta.Wcf.Contract.Entities;
using DavideTrotta.Wcf.Contract.Query;
using DavideTrotta.Wcf.Handlers.Infrastructure;

namespace DavideTrotta.Wcf.Handlers.Handlers
{
    public class GetContactByIdHandler : ICachedQueryHandler<GetContactByIdQuery, Contact>
    {
        public Contact Handle(GetContactByIdQuery query)
        {
            return new Contact
            {
                Id = 1,
                Lastname = "dasd",
                Name = "fasfsa"
            };
        }
    }
}
