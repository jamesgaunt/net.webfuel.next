using MediatR;
using Webfuel.Common;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain;

public class CreateTestProject : IRequest<Project>
{
}

internal class CreateTestProjectHandler : IRequestHandler<CreateTestProject, Project>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IConfigurationService _configurationService;
    private readonly IFileStorageService _fileStorageService;

    public CreateTestProjectHandler(
        IProjectRepository projectRepository,
        IConfigurationService configurationService,
        IFileStorageService fileStorageService)
    {
        _projectRepository = projectRepository;
        _configurationService = configurationService;
        _fileStorageService = fileStorageService;
    }

    public async Task<Project> Handle(CreateTestProject request, CancellationToken cancellationToken)
    {
        var existing = await _projectRepository.CountProject();
        if (existing > 0)
            throw new InvalidOperationException("Cannot create a test project when projects already exist.");

        var project = new Project();

        var fileStorageGroup = await _fileStorageService.CreateGroup();

        project.Number = await _configurationService.AllocateNextProjectNumber();
        project.PrefixedNumber = FormatPrefixedNumber(project);
        project.StatusId = ProjectStatusEnum.Active;
        project.CreatedAt = DateTimeOffset.UtcNow;

        project.SupportRequestId = null;
        project.FileStorageGroupId = fileStorageGroup.Id;

        await _projectRepository.InsertProject(project);

        return project;
    }

    string FormatPrefixedNumber(Project project)
    {
        return "IC" + project.Number.ToString("D5");
    }
}
