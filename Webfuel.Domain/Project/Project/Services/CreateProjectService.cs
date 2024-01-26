using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;
using Webfuel.Domain.Dashboard;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface ICreateProjectService
    {
        Task<Project> CreateProject(SupportRequest supportRequest);
    }

    [Service(typeof(ICreateProjectService))]
    internal class CreateProjectService : ICreateProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEnrichProjectService _enrichProjectService;
        private readonly IConfigurationService _configurationService;

        public CreateProjectService(
            IProjectRepository projectRepository, 
            IEnrichProjectService enrichProjectService,
            IConfigurationService configurationService)
        {
            _projectRepository = projectRepository;
            _enrichProjectService = enrichProjectService;
            _configurationService = configurationService;
        }

        public async Task<Project> CreateProject(SupportRequest supportRequest)
        {
            var project = new Project();

            new SupportRequestMapper().Apply(supportRequest, project);

            project.Number = await GetNextProjectNumber();
            project.PrefixedNumber = FormatPrefixedNumber(project);
            project.StatusId = ProjectStatusEnum.Active;
            project.CreatedAt = DateTimeOffset.UtcNow;
            project.SupportRequestId = supportRequest.Id;

            await _projectRepository.InsertProject(project);

            DashboardService.FlushProjectMetrics();

            return await _enrichProjectService.EnrichProject(project);
        }

        // Implementation

        async Task<int> GetNextProjectNumber()
        {
            return await _configurationService.AllocateNextProjectNumber();
        }

        string FormatPrefixedNumber(Project project)
        {
            return "IC" + project.Number.ToString("D5");
        }
    }
}