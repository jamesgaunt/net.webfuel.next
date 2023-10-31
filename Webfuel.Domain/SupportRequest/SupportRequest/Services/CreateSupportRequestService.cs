using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public CreateSupportRequestService(ISupportRequestRepository supportRequestRepository)
        {
            _supportRequestRepository = supportRequestRepository;
        }

        public async Task<SupportRequest> CreateSupportRequest(CreateSupportRequest request)
        {
            var supportRequest = new SupportRequestMapper().Map(request);

            supportRequest.StatusId = SupportRequestStatusEnum.ToBeTriaged;
            supportRequest.DateOfRequest = DateOnly.FromDateTime(DateTime.Now);
            supportRequest.CreatedAt = DateTimeOffset.UtcNow;

            return await _supportRequestRepository.InsertSupportRequest(supportRequest);
        }
    }
}
