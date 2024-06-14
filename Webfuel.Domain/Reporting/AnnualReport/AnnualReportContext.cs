using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    internal class AnnualReportContext
    {
        public required AnnualReportSettings Settings { get; init; }

        public required IStaticDataModel StaticData { get; init; }

        public required IReadOnlyList<User> Users { get; init; }

        public required IReadOnlyList<Researcher> Researchers { get; init; }

        public static async Task<AnnualReportContext> Create(IServiceProvider serviceProvider, AnnualReportSettings settings)
        {
            var result = new AnnualReportContext
            {
                Settings = settings,
                StaticData = await serviceProvider.GetRequiredService<IStaticDataService>().GetStaticData(),
                Users = await serviceProvider.GetRequiredService<IUserRepository>().SelectUser(),
                Researchers = await serviceProvider.GetRequiredService<IResearcherRepository>().SelectResearcher(),
            };
            return result;
        }
    }
}
