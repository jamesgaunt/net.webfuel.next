using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportBuilderMetrics
    {
        public long LoadSteps { get; private set; }
        public long LoadItems { get; private set; }
        public long LoadAvgMicroseconds => LoadSteps > 0 ? LoadMicroseconds / LoadSteps : 0;
        public long LoadAvgItems => LoadSteps > 0 ? LoadItems / LoadSteps : 0;
        public long LoadMicroseconds { get; private set; }
        public long LoadMinMicroseconds { get; private set; } = long.MaxValue;
        public long LoadMaxMicroseconds { get; private set; } = long.MinValue;

        public void AddLoad(long microseconds, long items)
        {
            LoadSteps++;
            LoadItems += items;
            LoadMicroseconds += microseconds;
            LoadMinMicroseconds = Math.Min(LoadMinMicroseconds, microseconds);
            LoadMaxMicroseconds = Math.Max(LoadMaxMicroseconds, microseconds);
        }

        public long RenderSteps { get; private set; }
        public long RenderItems { get; private set; }
        public long RenderMicroseconds { get; private set; }
        public long RenderAvgMicroseconds => RenderSteps > 0 ? RenderMicroseconds / RenderSteps : 0;
        public long RenderAvgItems => RenderSteps > 0 ? RenderItems / RenderSteps : 0;
        public long RenderMinMicroseconds { get; private set; } = long.MaxValue;
        public long RenderMaxMicroseconds { get; private set; } = long.MinValue;

        public void AddRender(long microseconds, long items)
        {
            RenderSteps++;
            RenderItems += items;
            RenderMicroseconds += microseconds;
            RenderMinMicroseconds = Math.Min(RenderMinMicroseconds, microseconds);
            RenderMaxMicroseconds = Math.Max(RenderMaxMicroseconds, microseconds);
        }

        public long GenerationSteps { get; private set; }
        public long GenerationItems { get; private set; }
        public long GenerationMicroseconds { get; private set; }
        public long GenerationAvgMicroseconds => GenerationSteps > 0 ? GenerationMicroseconds / GenerationSteps : 0;
        public long GenerationAvgItems => GenerationSteps > 0 ? GenerationItems / GenerationSteps : 0;
        public long GenerationMinMicroseconds { get; private set; } = long.MaxValue;
        public long GenerationMaxMicroseconds { get; private set; } = long.MinValue;

        public void AddGeneration(long microseconds, long items)
        {
            GenerationSteps++;
            GenerationItems += items;
            GenerationMicroseconds += microseconds;
            GenerationMinMicroseconds = Math.Min(GenerationMinMicroseconds, microseconds);
            GenerationMaxMicroseconds = Math.Max(GenerationMaxMicroseconds, microseconds);
        }
    }
}
