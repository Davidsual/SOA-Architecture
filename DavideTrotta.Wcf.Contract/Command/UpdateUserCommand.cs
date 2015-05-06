using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DavideTrotta.Wcf.Contract.Command
{
    [DataContract]
    public class UpdateUserCommand : Command
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
        [DataMember]
        [MinLength(10)]
        [Required]
        public string LastName { get; set; }
        [DataMember]
        public DateTime DateOfBirth { get; set; }

    }
}
