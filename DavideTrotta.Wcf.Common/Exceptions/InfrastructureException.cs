using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavideTrotta.Wcf.Common.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException()
        {
        }

        public InfrastructureException(string message)
            : base(message)
        {
        }

        public InfrastructureException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
