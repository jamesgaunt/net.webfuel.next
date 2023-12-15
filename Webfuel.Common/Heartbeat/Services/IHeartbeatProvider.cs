using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public interface IHeartbeatProvider
    {
        List<string> HeartbeatProviderNames { get; }

        Task<string> ExecuteHeartbeatAsync(Heartbeat heartbeat);
    }
}
