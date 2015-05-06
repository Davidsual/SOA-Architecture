using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DavideTrotta.Wcf.Contract.Command
{
    public interface ICommand
    { }

    [DataContract]
    [KnownType("GetKnownType")]
    public class Command : ICommand
    {
        public static IEnumerable<Type> GetKnownType()
        {
            return from type in typeof(ICommand).Assembly.GetTypes()
                   where typeof(ICommand).IsAssignableFrom(type)
                 select type;
        }

        public virtual bool IsValid(out ICollection<ValidationResult> validationResults)
        {            
            validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults, true);
            return !validationResults.Any();
        }
    }
}
