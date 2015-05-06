using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DavideTrotta.Wcf.Contract.Entities;

namespace DavideTrotta.Wcf.Contract.Query
{
    [DataContract]
    public class GetContactByIdQuery : Query<Contact>
    {
        [DataMember]
        [Required]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string AdditionalParameter { get; set; }
    }
}
