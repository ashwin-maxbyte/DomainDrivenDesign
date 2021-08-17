using DDD.Base.Validators.ValidatorAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DDD.Base.Validators
{
    public class BaseValidator<Validator, TransientEntity>
    {
        private static readonly ValidatorEntityAttribute _validatorEntityAttribute;
        private static readonly IEnumerable<MethodInfo> _runtimeMethods;

        static BaseValidator()
        {
            Type validatorType = typeof(Validator);
            _validatorEntityAttribute = validatorType.GetCustomAttribute<ValidatorEntityAttribute>();
            _runtimeMethods = validatorType.GetRuntimeMethods().Where(x => x.GetCustomAttributes(typeof(ValidationAttribute), false).Any()).ToList();
        }

        protected BaseValidator() { }

        public async Task Validate(TransientEntity transientEntity, long exclusionId)
        {
            object[] parameters = new object[] { transientEntity, exclusionId };
            foreach (var method in _runtimeMethods)
            {
                bool isValid;
                if(method.ReturnType == typeof(bool))
                {
                    isValid = RunSimpleValidator(method, parameters);
                }
                else
                {
                    isValid = await RunAsyncValidator(method, parameters);
                }

                if (!isValid)
                {
                    ThrowDataIntegrityException(method);
                }
            }
        }

        private bool RunSimpleValidator(MethodInfo method, object[] parameters)
        {
            return (bool)method.Invoke(this, parameters);
        }

        private async Task<bool> RunAsyncValidator(MethodInfo method, object[] parameters)
        {
            return await (Task<bool>)method.Invoke(this, parameters);
        }

        private static void ThrowDataIntegrityException(MethodInfo method)
        {
            ValidationAttribute validationAttribute = method.GetCustomAttribute<ValidationAttribute>();
            throw new DataIntegrityException(_validatorEntityAttribute.Entity, validationAttribute.TargetField, validationAttribute.ErrorMeessage);
        }
    }
}
