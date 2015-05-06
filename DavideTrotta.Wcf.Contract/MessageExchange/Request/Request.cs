using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DavideTrotta.Wcf.Contract.MessageExchange.Request
{
    [DataContract]
    public class Request
    {
        [DataMember]
        public Guid RequestId { get; set; }
    }
}
