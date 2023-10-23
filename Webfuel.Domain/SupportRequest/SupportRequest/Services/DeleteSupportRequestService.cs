using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public interface IDeleteSupportRequestService
    {
        Task DeleteSupportRequest(DeleteSupportRequest request);
    }

    [Service(typeof(IDeleteSupportRequestService))]
    internal class DeleteSupportRequestService: IDeleteSupportRequestService
    {
        private readonly ISupportRequestRepository _supportRequestRepository;

        public DeleteSupportRequestService(ISupportRequestRepository supportRequestRepository)
        {
            _supportRequestRepository = supportRequestRepository;
        }

        public async Task DeleteSupportRequest(DeleteSupportRequest request)
        {
            await _supportRequestRepository.DeleteSupportRequest(request.Id);
        }
    }
}
