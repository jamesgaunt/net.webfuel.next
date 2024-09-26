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
    public interface IProjectSubmissionFix
    {
        Task FixProjectSubmissions();
    }

    [Service(typeof(IProjectSubmissionFix))]
    internal class ProjectSubmissionFix: IProjectSubmissionFix
    {
        private readonly IProjectSubmissionRepository _projectSubmissionRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IUserSortService _userSortService;

        public ProjectSubmissionFix(
            IProjectSubmissionRepository projectSubmissionRepository,
            IProjectRepository projectRepository,
            IStaticDataService staticDataService,
            IUserSortService userSortService)
        {
            _projectSubmissionRepository = projectSubmissionRepository;
            _projectRepository = projectRepository;
            _staticDataService = staticDataService;
            _userSortService = userSortService;
        }

        public async Task FixProjectSubmissions()
        {
            var projects = await _projectRepository.SelectProject();
            var projectSubmissions = await _projectSubmissionRepository.SelectProjectSubmission();

            foreach(var original in projectSubmissions)
            {
                var updated = original.Copy();

                var project = projects.First(p => p.Id == original.ProjectId);

                updated.FundingStreamId = project.SubmittedFundingStreamId;
                updated.SubmissionStatusId = SubmissionStatusEnum.DonTKnow;

                await _projectSubmissionRepository.UpdateProjectSubmission(original: original, updated: updated);
            }
        }
    }

}
