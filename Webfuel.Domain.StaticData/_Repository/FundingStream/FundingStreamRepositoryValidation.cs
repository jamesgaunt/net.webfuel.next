using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    internal class FundingStreamRepositoryValidator: AbstractValidator<FundingStream>
    {
        public FundingStreamRepositoryValidator()
        {
            RuleFor(x => x.Name).Use(FundingStreamRepositoryValidationRules.Name);
            RuleFor(x => x.Code).Use(FundingStreamRepositoryValidationRules.Code);
        }
    }
    public static class FundingStreamRepositoryValidationRules
    {
        public const int Name_MaxLength = 64;
        public const int Code_MaxLength = 64;
        
        public static void Name<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Name_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Code<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Code_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
}

