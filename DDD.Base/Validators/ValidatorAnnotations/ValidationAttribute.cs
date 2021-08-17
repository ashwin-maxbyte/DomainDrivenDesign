using System;

namespace DDD.Base.Validators.ValidatorAnnotations
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidationAttribute : Attribute
    {
        public string TargetField { get; }
        public string ErrorMeessage { get; }

        public ValidationAttribute(string targetField, string errorMeessage)
        {
            TargetField = targetField;
            ErrorMeessage = errorMeessage;
        }
    }
}
