using System;
using System.Text;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public partial interface IProjectChangeLogService
    {
    }
    
    [Service(typeof(IProjectChangeLogService))]
    internal partial class ProjectChangeLogService: IProjectChangeLogService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IUserRepository _userRepository;
        
        public ProjectChangeLogService(IStaticDataService staticDataService, IUserRepository userRepository)
        {
            _staticDataService = staticDataService;
            _userRepository = userRepository;
        }
        
        public async Task<string> GenerateChangeLog(Project original, Project updated, string delimiter = "\n")
        {
            var staticData = await _staticDataService.GetStaticData();
            var sb = new StringBuilder();
            
            return sb.ToString();
        }
    }
}

