using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;
using Webfuel.Excel;

namespace Webfuel.Tools.ConsoleApp
{
    public interface IProjectFix
    {
        Task FixProjects();
    }

    [Service(typeof(IProjectFix))]
    internal class ProjectFix: IProjectFix
    {
        private readonly IEnrichProjectService _enrichProjectService;
        private readonly IProjectRepository _projectRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IUserSortService _userSortService;

        public ProjectFix(
            IEnrichProjectService enrichProjectService,
            IProjectRepository projectRepository, 
            IStaticDataService staticDataService,
            IUserSortService userSortService)
        {
            _enrichProjectService = enrichProjectService;
            _projectRepository = projectRepository;
            _staticDataService = staticDataService;
            _userSortService = userSortService;
        }

        public async Task FixProjects()
        {
            var projects = await _projectRepository.SelectProject();

            foreach(var project in projects)
            {
                await _enrichProjectService.EnrichProject(project);
            }
        }
    }

}
