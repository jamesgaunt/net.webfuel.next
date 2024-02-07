using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class UpdateReportHandler : IRequestHandler<UpdateReport, Report>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportDesignService _reportDesignService;
        private readonly IIdentityAccessor _identityAccessor;

        public UpdateReportHandler(IReportRepository reportRepository, IReportDesignService reportDesignService, IIdentityAccessor identityAccessor)
        {
            _reportRepository = reportRepository;
            _reportDesignService = reportDesignService;
            _identityAccessor = identityAccessor;
        }

        public async Task<Report> Handle(UpdateReport request, CancellationToken cancellationToken)
        {
            if(_identityAccessor.User == null)
                throw new DomainException("User not authenticated");

            await _reportDesignService.ValidateDesign(request.Design);

            var original = await _reportRepository.RequireReport(request.Id);

            if (original.OwnerUserId != _identityAccessor.User.Id)
                throw new DomainException("User does not have permission to update this report");

            if (original.ReportProviderId != request.Design.ReportProviderId)
                throw new DomainException("Cannot change the report provider of a report");

            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Description = request.Description;
            updated.IsPublic = request.IsPublic;
            updated.ReportGroupId = request.ReportGroupId;
            updated.Design = request.Design;

            return await _reportRepository.UpdateReport(original: original, updated: updated);
        }
    }
}
