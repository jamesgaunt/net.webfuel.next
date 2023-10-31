using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public interface IUpdateSupportRequestService
    {
        Task<SupportRequest> UpdateSupportRequest(UpdateSupportRequest request);

        Task<SupportRequest> UpdateSupportRequestResearcher(UpdateSupportRequestResearcher request);
    }

    [Service(typeof(IUpdateSupportRequestService))]
    internal class UpdateSupportRequestService: IUpdateSupportRequestService
    {
        private readonly ISupportRequestRepository _supportRequestRepository;

        public UpdateSupportRequestService(ISupportRequestRepository supportRequestRepository)
        {
            _supportRequestRepository = supportRequestRepository;
        }

        public async Task<SupportRequest> UpdateSupportRequest(UpdateSupportRequest request)
        {
            var original = await _supportRequestRepository.RequireSupportRequest(request.Id);

            var updated = original.Copy();

            new SupportRequestMapper().Apply(request, updated);

            return await _supportRequestRepository.UpdateSupportRequest(original: original, updated: updated);
        }

        public async Task<SupportRequest> UpdateSupportRequestResearcher(UpdateSupportRequestResearcher request)
        {
            var original = await _supportRequestRepository.RequireSupportRequest(request.Id);

            var updated = original.Copy();

            new SupportRequestMapper().Apply(request, updated);

            return await _supportRequestRepository.UpdateSupportRequest(original: original, updated: updated);
        }
    }
}
