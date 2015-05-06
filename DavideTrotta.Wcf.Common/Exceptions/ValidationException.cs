using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DavideTrotta.Wcf.Common.Exceptions
{
    public class ObjectValidationException : Exception
    {
        public IEnumerable<ValidationError> ValidationErrors { get; private set; }

        public ObjectValidationException(IEnumerable<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }

    public class ValidationError
    {
        public string ObjectName { get; set; }
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
