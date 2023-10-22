using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;

namespace Webfuel.Domain
{
    public interface ICreateProjectService
    {
        Task<Project> CreateProject(CreateProject command);
    }

    [Service(typeof(ICreateProjectService))]
    internal class CreateProjectService : ICreateProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IConfigurationService _configurationService;

        public CreateProjectService(IProjectRepository projectRepository, IConfigurationService configurationService)
        {
            _projectRepository = projectRepository;
            _configurationService = configurationService;
        }

        public async Task<Project> CreateProject(CreateProject request)
        {
            var project = new Project();


            project.Number = await GetNextProjectNumber();
            project.Title = request.Title;

            return await _projectRepository.InsertProject(project);
        }

        // Implementation

        async Task<int> GetNextProjectNumber()
        {
            return await _configurationService.AllocateNextProjectNumber();
        }
    }
}
