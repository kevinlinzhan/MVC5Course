using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.ValidationAttributeModel
{
    public class QualifiedAgeAttribute : ValidationAttribute
    {
        // Fields
        private int _qualifiedAge;

        // Constructors
        public QualifiedAgeAttribute(int qualifiedAge)
        {
            this._qualifiedAge = qualifiedAge;
        }

        // Methods
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime birthday = (DateTime)value;
            int age = new DateTime(DateTime.Now.Subtract(birthday).Ticks).Year - 1;

            if (age >= _qualifiedAge)
            {
                // valid
                return ValidationResult.Success;
            }
            else
            {
                // invalid
                var errorMsg = string.Format("Your age({0}) should be over {1}", age, _qualifiedAge);
                return new ValidationResult(errorMsg);
            }
        }
    }
}