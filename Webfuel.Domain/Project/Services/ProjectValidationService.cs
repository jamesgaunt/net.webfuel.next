using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface IProjectValidationService
    {
        Task ValidateProjectReferences(Project project);
    }

    [Service(typeof(IProjectValidationService))]
    internal class ProjectValidationService : IProjectValidationService
    {
        private readonly IStaticDataService _staticDataService;

        public ProjectValidationService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public async Task ValidateProjectReferences(Project project)
        {
            var staticData = await _staticDataService.GetStaticData();

            // TODO: Work out a decent way of naming these methods
            project.FundingBodyId =  staticData.FundingBody.GetOrDefault(project.FundingBodyId).Id;


        }
    }
}
