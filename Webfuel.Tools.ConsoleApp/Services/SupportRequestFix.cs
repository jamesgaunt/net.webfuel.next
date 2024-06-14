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
        private readonly IStaticDataService _staticDataService;
        private readonly IUserSortService _userSortService;

        public SupportRequestFix(
            ISupportRequestRepository supportRequestRepository, 
            IStaticDataService staticDataService,
            IUserSortService userSortService)
        {
            _supportRequestRepository = supportRequestRepository;
            _staticDataService = staticDataService;
            _userSortService = userSortService;
        }

        public async Task FixSupportRequests()
        {
            var supportRequests = await _supportRequestRepository.SelectSupportRequest();

            foreach(var original in supportRequests)
            {
                var updated = original.Copy();

                updated.TeamContactFullName = $"{updated.TeamContactTitle} {updated.TeamContactFirstName} {updated.TeamContactLastName}";
                updated.LeadApplicantFullName = $"{updated.LeadApplicantTitle} {updated.LeadApplicantFirstName} {updated.LeadApplicantLastName}";

                await _supportRequestRepository.UpdateSupportRequest(original: original, updated: updated);
            }
        }
    }
}
