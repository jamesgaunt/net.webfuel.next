using Webfuel.Common;
using Webfuel.Domain;

namespace Webfuel.Tools.ConsoleApp;

public interface ISupportMigration
{
    Task Migrate();
}

[Service(typeof(ISupportMigration))]
internal class SupportMigration : ISupportMigration
{
    private readonly IProjectRepository _projectRepository;
    private readonly ISupportRequestRepository _supportRequestRepository;
    private readonly IProjectSupportRepository _projectSupportRepository;
    private readonly IFileStorageGroupRepository _fileStorageGroupRepository;
    private readonly IProjectSupportGroupRepository _projectSupportGroupRepository;

    public SupportMigration(
        IProjectRepository projectRepository,
        ISupportRequestRepository supportRequestRepository,
        IProjectSupportRepository projectSupportRepository,
        IFileStorageGroupRepository fileStorageGroupRepository,
        IProjectSupportGroupRepository projectSupportGroupRepository)
    {
        _projectRepository = projectRepository;
        _supportRequestRepository = supportRequestRepository;
        _projectSupportRepository = projectSupportRepository;
        _fileStorageGroupRepository = fileStorageGroupRepository;
        _projectSupportGroupRepository = projectSupportGroupRepository;
    }

    public async Task Migrate()
    {
        var projects = await _projectRepository.SelectProject();
        var supportRequests = await _supportRequestRepository.SelectSupportRequest();
        var projectSupports = await _projectSupportRepository.SelectProjectSupport();
        var fileStorageGroups = await _fileStorageGroupRepository.SelectFileStorageGroup();
        var projectSupportGroups = await _projectSupportGroupRepository.SelectProjectSupportGroup();

        // Ensure every file  storage group has an associated project support group
        foreach (var fileStorageGroup in fileStorageGroups)
        {
            if (projectSupportGroups.Any(x => x.FileStorageGroupId == fileStorageGroup.Id))
                continue;

            var projectSupportGroup = new ProjectSupportGroup
            {
                Id = fileStorageGroup.Id,
                FileStorageGroupId = fileStorageGroup.Id,
                CreatedAt = DateTime.UtcNow,
            };
            projectSupportGroups.Add(await _projectSupportGroupRepository.InsertProjectSupportGroup(projectSupportGroup));
        }

        // Set ProjectSupportGroupId for each project
        foreach (var project in projects)
        {
            var originalProject = project.Copy();
            project.ProjectSupportGroupId = project.FileStorageGroupId;
            await _projectRepository.UpdateProject(updated: project, original: originalProject);
        }

        // Set ProjectSupportGroupId for each support request
        foreach (var supportRequest in supportRequests)
        {
            var originalSupportRequest = supportRequest.Copy();
            supportRequest.ProjectSupportGroupId = supportRequest.FileStorageGroupId;
            await _supportRequestRepository.UpdateSupportRequest(updated: supportRequest, original: originalSupportRequest);
        }

        // Set ProjectSupportGroupId for each project support
        foreach (var projectSupport in projectSupports)
        {
            var originalProjectSupport = projectSupport.Copy();
            var project = projects.FirstOrDefault(x => x.Id == projectSupport.ProjectId);
            if (project == null)
            {
                Console.WriteLine($"ProjectSupport with ProjectId {projectSupport.ProjectId} not found in projects.");
                continue;
            }

            projectSupport.ProjectSupportGroupId = project.FileStorageGroupId;
            await _projectSupportRepository.UpdateProjectSupport(updated: projectSupport, original: originalProjectSupport);
        }
    }
}
