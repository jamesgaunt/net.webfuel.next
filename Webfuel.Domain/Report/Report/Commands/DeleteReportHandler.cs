using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteReportHandler : IRequestHandler<DeleteReport>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public DeleteReportHandler(IReportRepository reportRepository, IIdentityAccessor identityAccessor)
        {
            _reportRepository = reportRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task Handle(DeleteReport request, CancellationToken cancellationToken)
        {
            if (_identityAccessor.User == null)
                throw new DomainException("User not authenticated");

            var original = await _reportRepository.RequireReport(request.Id);

            if (_identityAccessor.Claims.Developer == false && original.OwnerUserId != _identityAccessor.User.Id)
                throw new DomainException("User does not have permission to delete this report");

            await _reportRepository.DeleteReport(request.Id);
        }
    }
}
