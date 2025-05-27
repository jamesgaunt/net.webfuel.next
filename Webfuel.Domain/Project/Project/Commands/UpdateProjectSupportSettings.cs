using MediatR;

namespace Webfuel.Domain;

public class UpdateProjectSupportSettings : IRequest<Project>
{
    public required Guid Id { get; set; }

    public bool MockInterviews { get; set; }
    public bool GrantsmanshipReview { get; set; }
}

internal class UpdateProjectSupportSettingsHandler : IRequestHandler<UpdateProjectSupportSettings, Project>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectChangeLogService _projectChangeLogService;

    public UpdateProjectSupportSettingsHandler(
        IProjectRepository projectRepository,
        IProjectChangeLogService projectChangeLogService)
    {
        _projectRepository = projectRepository;
        _projectChangeLogService = projectChangeLogService;
    }

    public async Task<Project> Handle(UpdateProjectSupportSettings request, CancellationToken cancellationToken)
    {
        var original = await _projectRepository.RequireProject(request.Id);
        if (original.Locked)
            throw new InvalidOperationException("Unable to edit a locked project");

        var updated = ProjectMapper.Apply(request, original);

        updated = await _projectRepository.UpdateProject(original: original, updated: updated);

        await _projectChangeLogService.InsertChangeLog(original: original, updated: updated);
        return updated;
    }
}
