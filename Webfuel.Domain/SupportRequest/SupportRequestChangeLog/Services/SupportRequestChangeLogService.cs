using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface ISupportRequestChangeLogService
    {
        Task InsertChangeLog(SupportRequest original, SupportRequest updated, string action = "");
    }

    [Service(typeof(ISupportRequestChangeLogService))]
    internal partial class SupportRequestChangeLogService: ISupportRequestChangeLogService
    {
        private readonly ISupportRequestChangeLogRepository _supportRequestChangeLogRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IUserRepository _userRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public SupportRequestChangeLogService(
            ISupportRequestChangeLogRepository supportRequestChangeLogRepository,
            IStaticDataService staticDataService, 
            IUserRepository userRepository,
            IIdentityAccessor identityAccessor)
        {
            _supportRequestChangeLogRepository = supportRequestChangeLogRepository;
            _staticDataService = staticDataService;
            _userRepository = userRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task InsertChangeLog(SupportRequest original, SupportRequest updated, string action = "")
        {
            var message = await GenerateChangeLog(original: original, updated: updated);
            if (String.IsNullOrEmpty(message) && string.IsNullOrEmpty(action))
                return;

            if(!String.IsNullOrEmpty(action))
                message = action + ":\n" + message;

            var changeLog = new SupportRequestChangeLog
            {
                SupportRequestId = original.Id,
                Message = message,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedByUserId = _identityAccessor.User?.Id,
            };

            await _supportRequestChangeLogRepository.InsertSupportRequestChangeLog(changeLog);
        }
    }
}
