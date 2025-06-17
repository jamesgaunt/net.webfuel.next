using MediatR;
using Webfuel.Common;

namespace Webfuel.Domain;

public class QueryProjectSupport : Query, IRequest<QueryResult<ProjectSupport>>
{
    public required Guid ProjectSupportGroupId { get; set; }

    public bool OpenTeamSupportOnly { get; set; }

    public Query ApplyCustomFilters()
    {
        this.Equal(nameof(ProjectSupport.ProjectSupportGroupId), ProjectSupportGroupId);

        if (OpenTeamSupportOnly)
            this.SQL($"e.[SupportRequestedTeamId] IS NOT NULL AND e.[SupportRequestedCompletedAt] IS NULL");

        return this;
    }
}

internal class QueryProjectSupportHandler : IRequestHandler<QueryProjectSupport, QueryResult<ProjectSupport>>
{
    private readonly IProjectSupportGroupRepository _projectSupportGroupRepository;
    private readonly IProjectSupportRepository _projectSupportRepository;
    private readonly IFileStorageService _fileStorageService;

    public QueryProjectSupportHandler(
        IProjectSupportGroupRepository projectSupportGroupRepository,
        IProjectSupportRepository projectSupportRepository,
        IFileStorageService fileStorageService)
    {
        _projectSupportGroupRepository = projectSupportGroupRepository;
        _projectSupportRepository = projectSupportRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<QueryResult<ProjectSupport>> Handle(QueryProjectSupport request, CancellationToken cancellationToken)
    {
        var projectSupportGroup = await _projectSupportGroupRepository.RequireProjectSupportGroup(request.ProjectSupportGroupId);
        var files = await _fileStorageService.QueryFiles(new QueryFileStorageEntry
        {
            FileStorageGroupId = projectSupportGroup.FileStorageGroupId,
            Take = 1000
        });

        var result = await _projectSupportRepository.QueryProjectSupport(request.ApplyCustomFilters());

        // Check all the files attached to these project support items still exist in the file storage group
        // We just clear them from the UI, they don't get deleted from the database, maybe we will do this offline at a later time
        foreach (var item in result.Items)
        {
            if (item.Files.Count > 0)
            {
                var toRemove = new List<ProjectSupportFile>();
                foreach (var file in item.Files)
                {
                    var fileEntry = files.Items.FirstOrDefault(f => f.Id == file.Id);
                    if (fileEntry == null)
                    {
                        toRemove.Add(file);
                    }
                }
                foreach (var file in toRemove)
                {
                    item.Files.Remove(file);
                }
            }
        }
        return result;
    }
}
