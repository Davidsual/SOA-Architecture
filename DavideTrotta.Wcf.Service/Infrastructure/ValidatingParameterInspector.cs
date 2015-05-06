using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.Dispatcher;
using DavideTrotta.Wcf.Common.Exceptions;
using DavideTrotta.Wcf.Contract.Exception;
using System.ServiceModel;

namespace DavideTrotta.Wcf.Service.Infrastructure
{
    public class ValidatingParameterInspector : IParameterInspector
    {
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState) { }

        public object BeforeCall(string operationName, object[] inputs)
        {
            var outputDict = new List<DavideTrotta.Wcf.Common.Exceptions.ValidationError>();

            //So I loop through all the input parameters and validate it using DataAnnotations Validator
            foreach (var input in inputs)
            {
                if (input != null)
                {
                    var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                    var context = new ValidationContext(input, null, null);
                    var isValid = Validator.TryValidateObject(input, context, results, true);

                    if (!isValid)
                    {
                        results.ForEach(item => outputDict.Add(new DavideTrotta.Wcf.Common.Exceptions.ValidationError
                        {
                            ErrorMessage = item.ErrorMessage,
                            ObjectName = input.GetType().Name,
                            PropertyName = item.MemberNames.FirstOrDefault()
                        }));
                    }
                }
            }

            if (outputDict.Any())
            {
                throw new ObjectValidationException(outputDict);
            }
            return null;

        }
    }
}
