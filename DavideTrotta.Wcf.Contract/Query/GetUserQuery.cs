using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavideTrotta.Wcf.Contract.Query
{
    public class GetUserQuery : IQuery<GetUserResult>
    {
        public int Id { get; set; }
    }

    public class GetUserResult
    {
        public string Name { get; set; }
    }
}
