using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DavideTrotta.Wcf.Contract.Command
{
    [DataContract]
    public class CreateUserCommand : Command
    {
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
