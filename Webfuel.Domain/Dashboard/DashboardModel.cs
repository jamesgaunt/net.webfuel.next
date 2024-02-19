using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class DashboardModel
    {
        public required List<DashboardMetric> SupportMetrics { get; init; }

        public required List<DashboardMetric> ProjectMetrics { get; init; }
    }

    public class DashboardMetric
    {
        public string Name { get; init; } = String.Empty;

        public int? Count { get; init; }

        public string Icon { get; init; } = String.Empty;

        public string CTA { get; init; } = String.Empty;

        public string RouterLink { get; init; } = String.Empty;

        public string RouterParams { get; init; } = String.Empty;

        public string BackgroundColor { get; init; } = String.Empty;
    }
}
