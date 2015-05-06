using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DavideTrotta.Wcf.Contract.Exception
{
    [DataContract]
    public class SimpleErrorFault
    {
        [DataMember]
        public string Test { get; set; }
    }
}
