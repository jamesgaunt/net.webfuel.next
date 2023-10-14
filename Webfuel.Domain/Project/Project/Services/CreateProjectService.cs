using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public CreateProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> CreateProject(CreateProject request)
        {
            var project = new Project();

            project.Title = request.Title;

            return await _projectRepository.InsertProject(project);
        }
    }
}
