using FluentValidation;

namespace Webfuel;

public static class ValidationExtensions
{
    public static IRuleBuilder<TEntity, TValue> Use<TEntity, TValue>(this IRuleBuilder<TEntity, TValue> ruleBuilder, Action<IRuleBuilder<TEntity, TValue>> action)
    {
        action(ruleBuilder);
        return ruleBuilder;
    }
}
