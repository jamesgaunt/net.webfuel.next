using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public partial class UserActivityRepositoryValidator
    {
        partial void Validation()
        {
            RuleFor(m => m).Must(m =>
            {
                if(m.WorkActivityId == null)
                {
                    return
                        m.ProjectId.HasValue &&
                        m.ProjectSupportId.HasValue &&
                        !String.IsNullOrEmpty(m.ProjectPrefixedNumber);
                }

                if(m.ProjectId == null)
                {
                    return
                        m.WorkActivityId.HasValue;
                }

                return false; 
            })
                .WithMessage("User activity must represent work activity or project support.");
        }
    }
}
