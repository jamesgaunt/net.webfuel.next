using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public interface ICleanupService
    {
    }

    class CleanupProvider
    {
        public CleanupProvider(Type type)
        {
            Type = type;
        }

        public Type Type { get; set; }

        public DateTime LastRun { get; set; }

        public DateTime? DeferUntil { get; set; }
    }

    [Service(typeof(ICleanupService), typeof(IHeartbeatProvider))]
    class CleanupService : ICleanupService, IHeartbeatProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public CleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public List<string> HeartbeatProviderNames => new List<String> { "CleanupService.Cleanup" };

        public async Task<string> ExecuteHeartbeatAsync(Heartbeat heartbeat)
        {
            var services = _serviceProvider.GetServices<ICleanupProvider>();

            if (Providers.Count == 0)
            {
                foreach (var s in services)
                    Providers.Add(new CleanupProvider(s.GetType()));
            }

            var provider = Providers
                .Where(p => p.DeferUntil == null || p.DeferUntil.Value < DateTime.Now)
                .OrderBy(p => p.LastRun)
                .FirstOrDefault();

            if (provider == null)
                return String.Empty;

            var service = services.FirstOrDefault(p => p.GetType() == provider.Type);
            if (service == null)
                return "Unable to find service provider";

            var count = await service.CleanupAsync();
            provider.LastRun = DateTime.Now;

            if(count == 0)  
                provider.DeferUntil = DateTime.Now.AddHours(2);

            return $"{provider.Type.Name} cleaned up {count} items";
        }

        static List<CleanupProvider> Providers = new List<CleanupProvider>();
    }
}
