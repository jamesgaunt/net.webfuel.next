using FluentValidation;
using MediatR;

namespace Webfuel.Common
{
    public class UpdateHeartbeat : IRequest<Heartbeat>
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }

        public required bool Live { get; init; }

        public required bool LogSuccessfulExecutions { get; init; }

        public required string ProviderName { get; init; }

        public string ProviderParameter { get; init; } = string.Empty;

        public string MinTime { get; init; } = string.Empty;

        public string MaxTime { get; init; } = string.Empty;

        public string Schedule { get; init; } = string.Empty;
    }
}
