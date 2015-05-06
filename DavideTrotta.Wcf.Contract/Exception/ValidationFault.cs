using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DavideTrotta.Wcf.Contract.Exception
{
    [DataContract]
    public class ValidationFault
    {
        [DataMember]
        public IEnumerable<ValidationError> Errors { get; private set; }

        public ValidationFault(IEnumerable<ValidationError> errors)
        {
            Errors = errors;
        }
    }

    [DataContract]
    public class ValidationError
    {
        [DataMember]
        public string ObjectName { get; set; }
        [DataMember]
        public string PropertyName { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
