using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface ICreateSupportRequestService
    {
        Task<SupportRequest> CreateSupportRequest(CreateSupportRequest request);
    }

    [Service(typeof(ICreateSupportRequestService))]
    internal class CreateSupportRequestService : ICreateSupportRequestService
    {
        private readonly ISupportRequestRepository _supportRequestRepository;
        private readonly IConfigurationService _configurationService;

        public CreateSupportRequestService(ISupportRequestRepository supportRequestRepository, IConfigurationService configurationService)
        {
            _supportRequestRepository = supportRequestRepository;
            _configurationService = configurationService;
        }

        public async Task<SupportRequest> CreateSupportRequest(CreateSupportRequest request)
        {
            var supportRequest = new SupportRequestMapper().Map(request);

            supportRequest.Number = await GetNextSupportRequestNumber();
            supportRequest.PrefixedNumber = FormatPrefixedNumber(supportRequest);
            supportRequest.StatusId = SupportRequestStatusEnum.ToBeTriaged;
            supportRequest.DateOfRequest = DateOnly.FromDateTime(DateTime.Now);
            supportRequest.CreatedAt = DateTimeOffset.UtcNow;

            return await _supportRequestRepository.InsertSupportRequest(supportRequest);
        }

        // Implementation

        async Task<int> GetNextSupportRequestNumber()
        {
            return await _configurationService.AllocateNextProjectNumber();
        }

        string FormatPrefixedNumber(SupportRequest supportRequest)
        {
            return "SR" + supportRequest.Number.ToString("D5");
        }
    }
}
