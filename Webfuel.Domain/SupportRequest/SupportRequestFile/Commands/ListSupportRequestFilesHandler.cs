using MediatR;
using Webfuel.Common;

namespace Webfuel.Domain
{
    internal class ListSupportRequestFilesHandler : IRequestHandler<ListSupportRequestFiles, List<FileStorageEntry>>
    {
        private readonly ISupportRequestRepository _supportRequestRepository;
        private readonly IFileStorageService _fileStorageService;

        public ListSupportRequestFilesHandler(
            ISupportRequestRepository supportRequestRepository,
            IFileStorageService fileStorageService)
        {
            _supportRequestRepository = supportRequestRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<List<FileStorageEntry>> Handle(ListSupportRequestFiles request, CancellationToken cancellationToken)
        {
            var supportRequest = await _supportRequestRepository.RequireSupportRequest(request.SupportRequestId);
            return await _fileStorageService.ListFiles(supportRequest.FileStorageGroupId);
        }
    }
}
