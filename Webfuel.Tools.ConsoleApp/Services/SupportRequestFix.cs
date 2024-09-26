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
    public interface ISupportRequestFix
    {
        Task FixSupportRequests();
    }

    [Service(typeof(ISupportRequestFix))]
    internal class SupportRequestFix: ISupportRequestFix
    {
        private readonly ISupportRequestRepository _supportRequestRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IUserSortService _userSortService;

        public SupportRequestFix(
            ISupportRequestRepository supportRequestRepository, 
            IProjectRepository projectRepository,
            IStaticDataService staticDataService,
            IUserSortService userSortService)
        {
            _supportRequestRepository = supportRequestRepository;
            _projectRepository = projectRepository;
            _staticDataService = staticDataService;
            _userSortService = userSortService;
        }

        public async Task FixSupportRequests()
        {
            var projects = await _projectRepository.SelectProject();
            var supportRequests = await _supportRequestRepository.SelectSupportRequest();

            foreach(var original in supportRequests)
            {
                if (original.ProjectId == null)
                    continue;

                var updated = original.Copy();

                var project = projects.First(p => p.Id == original.ProjectId.Value);

                updated.DateOfTriage = DateOnly.FromDateTime(project.CreatedAt.DateTime);

                await _supportRequestRepository.UpdateSupportRequest(original: original, updated: updated);
            }
        }
    }
}
