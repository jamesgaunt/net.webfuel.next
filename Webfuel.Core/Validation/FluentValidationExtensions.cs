using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilder<TEntity, TValue> Use<TEntity, TValue>(this IRuleBuilder<TEntity, TValue> ruleBuilder, Action<IRuleBuilder<TEntity, TValue>> action)
        {
            action(ruleBuilder);
            return ruleBuilder;
        }
    }
}
