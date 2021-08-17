using System;

namespace DDD.Base.Validators.ValidatorAnnotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidatorEntityAttribute : Attribute
    {
        public string Entity { get; }

        public ValidatorEntityAttribute(string entity)
        {
            Entity = entity;
        }
    }
}
