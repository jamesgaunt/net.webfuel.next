

namespace Webfuel.Domain.StaticData
{
    public partial interface IStaticDataService : IClientConfigurationProvider
    {
        Task<IStaticDataModel> GetStaticData();
    }

    [Service(typeof(IStaticDataService), typeof(IClientConfigurationProvider))]
    internal partial class StaticDataService : IStaticDataService
    {
        private readonly IStaticDataCache _staticDataCache;
        private readonly IIdentityAccessor _identityAccessor;

        public StaticDataService(
            IStaticDataCache staticDataCache,
            IIdentityAccessor identityAccessor)
        {
            _staticDataCache = staticDataCache;
            _identityAccessor = identityAccessor;
        }

        public async Task<IStaticDataModel> GetStaticData()
        {
            var model = await _staticDataCache.GetStaticData();
            return model;
        }

        public Task ProvideClientConfiguration(ClientConfiguration clientConfiguration)
        {
            if (_identityAccessor.Claims.CanEditStaticData)
            {
                clientConfiguration.StaticDataMenu.AddChild(name: "Application Stage", action: "/static-data/application-stage");
                clientConfiguration.StaticDataMenu.AddChild(name: "Project Status", action: "/static-data/project-status");
                clientConfiguration.StaticDataMenu.AddChild(name: "Submission Stage", action: "/static-data/submission-stage");
                clientConfiguration.StaticDataMenu.AddChild(name: "Title", action: "/static-data/title");
                clientConfiguration.StaticDataMenu.AddChild(name: "Gender", action: "/static-data/gender");
                clientConfiguration.StaticDataMenu.AddChild(name: "Age Range", action: "/static-data/age-range");
                clientConfiguration.StaticDataMenu.AddChild(name: "Ethnicity", action: "/static-data/ethnicity");
                clientConfiguration.StaticDataMenu.AddChild(name: "Disability", action: "/static-data/disability");
                clientConfiguration.StaticDataMenu.AddChild(name: "Comms", action: "/static-data/comms");
                clientConfiguration.StaticDataMenu.AddChild(name: "Funding Stream", action: "/static-data/funding-stream");
                clientConfiguration.StaticDataMenu.AddChild(name: "Funding Body", action: "/static-data/funding-body");
                clientConfiguration.StaticDataMenu.AddChild(name: "Funding Call Type", action: "/static-data/funding-call-type");
                clientConfiguration.StaticDataMenu.AddChild(name: "Support Provided", action: "/static-data/support-provided");
                clientConfiguration.StaticDataMenu.AddChild(name: "Work Activity", action: "/static-data/work-activity");
                clientConfiguration.StaticDataMenu.AddChild(name: "User Discipline", action: "/static-data/user-discipline");
            }
            return Task.CompletedTask;
        }
    }
}

