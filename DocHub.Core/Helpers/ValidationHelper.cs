using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.Helpers
{
    public class ValidationHelper
    {
        public static void ModelValidation(object o)
        {
            ValidationContext validation = new ValidationContext(o);
            List <ValidationResult> results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(o, validation, results, true);
            if (!isValid) { throw new ArgumentException(results?.FirstOrDefault()?.ErrorMessage); }
        }
    }
}
