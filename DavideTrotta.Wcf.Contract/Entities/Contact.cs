using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DavideTrotta.Wcf.Contract.Entities
{
    [DataContract]
    public class Contact
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Lastname { get; set; }
    }
}
