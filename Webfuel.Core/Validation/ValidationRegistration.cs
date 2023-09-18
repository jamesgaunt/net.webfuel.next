using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandValidatorAttribute: Attribute
    {
    }

    public static class ValidationRegistration
    {
        public static void RegisterCommandValidatorsFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var registeredTypes = new Dictionary<Type, Type>();

            foreach (var type in assembly.DefinedTypes)
            {
                var attribute = type.GetCustomAttribute<CommandValidatorAttribute>();
                if (attribute == null)
                    continue;

                if (type.BaseType == null || !type.BaseType.IsGenericType || type.BaseType.GetGenericTypeDefinition() != typeof(AbstractValidator<>))
                    throw new InvalidOperationException("Command Validator does not implement AbstractValidator<>");

                var validatedType = type.BaseType.GetGenericArguments()[0];

                if (registeredTypes.ContainsKey(validatedType))
                    throw new InvalidOperationException("Multiple Command Validators registered for " + validatedType.Name);
                registeredTypes.Add(validatedType, type);

                services.AddTransient(typeof(IValidator<>).MakeGenericType(validatedType), type);
            }
        }
    }
}
