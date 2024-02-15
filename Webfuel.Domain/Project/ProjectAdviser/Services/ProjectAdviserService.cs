using DocumentFormat.OpenXml.Office2010.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;
using Webfuel.Domain.Dashboard;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface IProjectAdviserService
    {
        Task<List<Guid>> SelectProjectAdviserUserIdsByProjectId(Guid projectId);

        Task UpdateProjectAdvisers(Guid projectId, List<Guid> projectAdviserUserIds);
    }

    [Service(typeof(IProjectAdviserService))]
    internal class ProjectAdviserService : IProjectAdviserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectAdviserRepository _projectAdviserRepository;
        private readonly IEmailTemplateService _emailTemplateService;

        public ProjectAdviserService(
            IUserRepository userRepository,
            IProjectRepository projectRepository,
            IProjectAdviserRepository projectAdviserRepository,
            IEmailTemplateService emailTemplateService)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _projectAdviserRepository = projectAdviserRepository;
            _emailTemplateService = emailTemplateService;
        }

        public async Task<List<Guid>> SelectProjectAdviserUserIdsByProjectId(Guid projectId)
        {
            return (await _projectAdviserRepository.SelectProjectAdviserByProjectId(projectId))
                .Select(x => x.UserId)
                .ToList();
        }

        public async Task UpdateProjectAdvisers(Guid projectId, List<Guid> projectAdviserUserIds)
        {
            var project = await _projectRepository.GetProject(projectId);
            if (project == null)
                return;

            var existing = await _projectAdviserRepository.SelectProjectAdviserByProjectId(projectId);

            // Delete
            foreach(var adviser in existing)
            {
                if (!projectAdviserUserIds.Contains(adviser.UserId))
                {
                    await _projectAdviserRepository.DeleteProjectAdviser(adviser.Id);
                }
            }

            // Insert
            foreach(var userId in projectAdviserUserIds)
            {
                if (!existing.Any(x => x.UserId == userId))
                {
                    var user = await _userRepository.GetUser(userId);
                    if (user == null)
                        continue;

                    await _projectAdviserRepository.InsertProjectAdviser(new ProjectAdviser
                    {
                        ProjectId = projectId,
                        UserId = userId
                    });

                    await SendSupportAdviserAssignedEmail(project, user);
                }
            }
        }

        public async Task SendSupportAdviserAssignedEmail(Project project, User user)
        {
            var replacements = new Dictionary<string, string>
            {
                { "PROJECT_TITLE", project.Title },
                { "PROJECT_REFERENCE", project.PrefixedNumber },
                { "ADVISER_NAME", user.FullName },
                { "ADVISER_EMAIL", user.Email }
            };  

            await _emailTemplateService.SendEmail("Support Adviser Assigned", replacements);
        }
    }
}
