using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface IProjectChangeLogService
    {
        Task InsertChangeLog(Project original, Project updated);
    }

    [Service(typeof(IProjectChangeLogService))]
    internal partial class ProjectChangeLogService: IProjectChangeLogService
    {
        private readonly IProjectChangeLogRepository _projectChangeLogRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IUserRepository _userRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public ProjectChangeLogService(
            IProjectChangeLogRepository projectChangeLogRepository,
            IStaticDataService staticDataService, 
            IUserRepository userRepository,
            IIdentityAccessor identityAccessor)
        {
            _projectChangeLogRepository = projectChangeLogRepository;
            _staticDataService = staticDataService;
            _userRepository = userRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task InsertChangeLog(Project original, Project updated)
        {
            var message = await GenerateChangeLog(original: original, updated: updated);
            if (String.IsNullOrEmpty(message))
                return;

            var changeLog = new ProjectChangeLog
            {
                ProjectId = original.Id,
                Message = message,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedByUserId = _identityAccessor.User?.Id,
            };

            await _projectChangeLogRepository.InsertProjectChangeLog(changeLog);
        }
    }
}
