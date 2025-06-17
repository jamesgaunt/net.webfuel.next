using MediatR;
using Microsoft.AspNetCore.Http;
using Webfuel.Common;

namespace Webfuel.Domain;

public class UploadProjectSupportFile : IRequest<ProjectSupportFile>
{
    public required Guid ProjectSupportGroupId { get; set; }

    public required IFormFile FormFile { get; set; }
}

internal class UploadProjectSupportFileHandler : IRequestHandler<UploadProjectSupportFile, ProjectSupportFile>
{
    private readonly IProjectSupportGroupRepository _projectSupportGroupRepository;
    private readonly IFileStorageService _fileStorageService;

    public UploadProjectSupportFileHandler(
        IProjectSupportGroupRepository projectSupportGroupRepository,
        IFileStorageService fileStorageService)
    {
        _projectSupportGroupRepository = projectSupportGroupRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<ProjectSupportFile> Handle(UploadProjectSupportFile request, CancellationToken cancellationToken)
    {
        var projectSupportGroup = await _projectSupportGroupRepository.RequireProjectSupportGroup(request.ProjectSupportGroupId);

        var result = await _fileStorageService.UploadFile(new UploadFileStorageEntry
        {
            FileStorageGroupId = projectSupportGroup.FileStorageGroupId,
            FormFile = request.FormFile,
        });

        return new ProjectSupportFile
        {
            Id = result.Id,
            FileName = result.FileName,
            SizeBytes = result.SizeBytes,
            UploadedAt = result.UploadedAt
        };
    }
}
