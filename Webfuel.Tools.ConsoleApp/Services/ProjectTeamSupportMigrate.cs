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
    public interface IProjectTeamSupportMigrate
    {
        Task MigrateProjectTeamSupports();
    }

    [Service(typeof(IProjectTeamSupportMigrate))]
    internal class ProjectTeamSupportMigrate : IProjectTeamSupportMigrate
    {
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IProjectTeamSupportRepository _projectTeamSupportRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IUserSortService _userSortService;

        public ProjectTeamSupportMigrate(
            IProjectSupportRepository projectSupportRepository, 
            IStaticDataService staticDataService,
            IUserSortService userSortService,
            IProjectTeamSupportRepository projectTeamSupportRepository)
        {
            _projectSupportRepository = projectSupportRepository;
            _staticDataService = staticDataService;
            _userSortService = userSortService;
            _projectTeamSupportRepository = projectTeamSupportRepository;
        }

        public async Task MigrateProjectTeamSupports()
        {
            var projectTeamSupports = await _projectTeamSupportRepository.SelectProjectTeamSupport();

            foreach(var pts in projectTeamSupports)
            {
                var ps = new ProjectSupport
                {
                    Id = pts.Id,
                    Date = DateOnly.FromDateTime(pts.CreatedAt.Date),
                    ProjectId = pts.ProjectId,
                    Description = pts.CreatedNotes,
                    WorkTimeInHours = 0,
                    AdviserIds = new List<Guid> { pts.CreatedByUserId },

                    SupportRequestedTeamId = pts.SupportTeamId,
                    SupportRequestedCompletedAt = pts.CompletedAt,
                    SupportRequestedCompletedByUserId = pts.CompletedByUserId,
                    SupportRequestedCompletedNotes = pts.CompletedNotes,
                };

                await _projectSupportRepository.InsertProjectSupport(ps);
            }
        }
    }

}
