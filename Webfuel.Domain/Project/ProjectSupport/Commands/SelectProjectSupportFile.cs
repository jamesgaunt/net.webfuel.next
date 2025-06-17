using MediatR;
using Webfuel.Common;

namespace Webfuel.Domain;

public class SelectProjectSupportFile : IRequest<List<ProjectSupportFile>>
{
    public required Guid ProjectSupportGroupId { get; set; }
}

internal class SelectProjectSupportFileHandler : IRequestHandler<SelectProjectSupportFile, List<ProjectSupportFile>>
{
    private readonly IProjectSupportGroupRepository _projectSupportGroupRepository;
    private readonly IFileStorageService _fileStorageService;

    public SelectProjectSupportFileHandler(
        IProjectSupportGroupRepository projectSupportGroupRepository,
        IFileStorageService fileStorageService)
    {
        _projectSupportGroupRepository = projectSupportGroupRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<List<ProjectSupportFile>> Handle(SelectProjectSupportFile request, CancellationToken cancellationToken)
    {
        var projectSupportGroup = await _projectSupportGroupRepository.RequireProjectSupportGroup(request.ProjectSupportGroupId);

        var files = await _fileStorageService.QueryFiles(new QueryFileStorageEntry
        {
            FileStorageGroupId = projectSupportGroup.FileStorageGroupId,
            Skip = 0,
            Take = 200
        });

        return files.Items.Select(p => new ProjectSupportFile
        {
            Id = p.Id,
            FileName = p.FileName,
            SizeBytes = p.SizeBytes,
            UploadedAt = p.UploadedAt
        }).ToList();
    }
}
