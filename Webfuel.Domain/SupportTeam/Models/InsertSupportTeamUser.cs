using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class InsertSupportTeamUser : IRequest
    {
        public Guid SupportTeamId { get; set; }

        public Guid UserId { get; set; }
    }
}
