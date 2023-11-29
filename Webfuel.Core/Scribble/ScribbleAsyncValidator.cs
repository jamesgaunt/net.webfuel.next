using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    /// <summary>
    /// Assigns a custom async validator to a method
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ScribbleAsyncValidatorAttribute: Attribute
    {
        public readonly Type ValidatorType;

        public ScribbleAsyncValidatorAttribute(Type validatorType)
        {
            ValidatorType = validatorType;
        }
    }

    public interface IScribbleAsyncValidator
    {
        Task<string?> ValidateAsync(MethodInfo method, Dictionary<string, object?> literalParameters, object? validationContext);
    }
}
