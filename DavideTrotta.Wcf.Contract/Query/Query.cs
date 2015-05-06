using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DavideTrotta.Wcf.Contract.Query
{
    [DataContract]
    public class Query<TResponse> : IQuery<TResponse>
    {
        public virtual bool IsValid(out ICollection<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults, true);
            return !validationResults.Any();
        }
    }
}
