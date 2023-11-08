using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.ValidationAttributes
{
    public class PeselNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            string? pesel = value.ToString();
            

            if (pesel?.Length != 11 || !IsNumeric(pesel))
            {
                return new ValidationResult("The PESEL number must consist of 11 digits");
            }
            var peselAsArray = pesel.ToCharArray();
            /*
             */
            int controlSum = 0;
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            for (int i = 0; i < 10; i++)
            {
                var tmp = Convert.ToInt32(peselAsArray[i].ToString());
                controlSum += tmp * weights[i];
            }
            int lastDigit = Convert.ToInt32(peselAsArray[10].ToString());
            controlSum %= 10;
            controlSum = 10 - controlSum;
            controlSum %= 10;

            if (controlSum == lastDigit)
                return ValidationResult.Success;
            else
                return new ValidationResult("Incorrect sum for checking the PESEL number");


        }

        private bool IsNumeric(string pesel)
        {
            foreach (char c in pesel)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
