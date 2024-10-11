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
    public interface IProjectSupportFix
    {
        Task FixProjectSupports();
    }

    [Service(typeof(IProjectSupportFix))]
    internal class ProjectSupportFix: IProjectSupportFix
    {
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IUserSortService _userSortService;

        public ProjectSupportFix(
            IProjectSupportRepository projectSupportRepository,
            IProjectRepository projectRepository,
            IStaticDataService staticDataService,
            IUserSortService userSortService)
        {
            _projectSupportRepository = projectSupportRepository;
            _projectRepository = projectRepository;
            _staticDataService = staticDataService;
            _userSortService = userSortService;
        }

        public async Task FixProjectSupports()
        {
            var projectSupports = await _projectSupportRepository.SelectProjectSupport();

            foreach(var original in projectSupports)
            {
                var updated = original.Copy();

                // Convert plain text support description to HTML 

                updated.Description = updated.Description.Replace("\n", "<br/>");

                await _projectSupportRepository.UpdateProjectSupport(original: original, updated: updated);
            }
        }
    }
}
