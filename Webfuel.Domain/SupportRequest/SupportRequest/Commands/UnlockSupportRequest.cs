using MediatR;
using System.Diagnostics.CodeAnalysis;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public class UnlockSupportRequest : IRequest<SupportRequest>
    {
        public required Guid Id { get; set; }
    }

    internal class UnlockSupportRequestHandler : IRequestHandler<UnlockSupportRequest, SupportRequest>
    {
        private readonly ISupportRequestRepository _supportRequestRepository;
        private readonly ISupportRequestChangeLogService _supportRequestChangeLogService;

        public UnlockSupportRequestHandler(
            ISupportRequestRepository supportRequestRepository, 
            ISupportRequestChangeLogService supportRequestChangeLogService)
        {
            _supportRequestRepository = supportRequestRepository;
            _supportRequestChangeLogService = supportRequestChangeLogService;
        }

        public async Task<SupportRequest> Handle(UnlockSupportRequest request, CancellationToken cancellationToken)
        {
            var original = await _supportRequestRepository.RequireSupportRequest(request.Id);

            if (original.StatusId == SupportRequestStatusEnum.ToBeTriaged)
                return original; // Nothing to do

            var updated = original.Copy();

            updated.StatusId = SupportRequestStatusEnum.ToBeTriaged;
            updated.TriageNote = String.Empty;

            updated =  await _supportRequestRepository.UpdateSupportRequest(updated: updated, original: original);

            await _supportRequestChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }
    }
}