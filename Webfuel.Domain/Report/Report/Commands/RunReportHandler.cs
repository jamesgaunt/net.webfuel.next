using DocumentFormat.OpenXml.Presentation;
using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class RunReportHandler : IRequestHandler<RunReport, ReportStep>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IReportDesignService _reportDesignService;

        public RunReportHandler(IReportRepository reportRepository, IIdentityAccessor identityAccessor, IReportDesignService reportDesignService)
        {
            _reportRepository = reportRepository;
            _identityAccessor = identityAccessor;
            _reportDesignService = reportDesignService;
        }

        public async Task<ReportStep> Handle(RunReport request, CancellationToken cancellationToken)
        {
            if (_identityAccessor.User == null)
                throw new DomainException("User not authenticated");

            var report = await _reportRepository.RequireReport(request.ReportId);

            
            if (_identityAccessor.Claims.Developer == false && report.IsPublic == false && report.OwnerUserId != _identityAccessor.User.Id)
                throw new DomainException("User does not have permission to run this report");

            return _reportDesignService.RegisterReport(new ReportRequest
            {
                ReportName = report.Name,
                Design = report.Design,
                Arguments = request.Arguments ?? new List<ReportArgument>(),
                TypedArguments = request.TypedArguments
            });
        }
    }
}
