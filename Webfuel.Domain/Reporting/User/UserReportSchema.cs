using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public static class UserReportSchema
    {
        static UserReportSchema()
        {
            var builder = new ReportSchemaBuilder<UserReportContext>();

            builder.AddField(Guid.Parse("c077475e-16cd-484a-9e61-516550b92143"), p => p.Item.Email);
            builder.AddField(Guid.Parse("21be5330-cdfd-4a73-9d42-2c1d6ea258ae"), p => p.Item.Title);
            builder.AddField(Guid.Parse("54f8f29e-926b-4b26-a0d5-cb08f4fcdf09"), p => p.Item.FirstName);
            builder.AddField(Guid.Parse("755f3b11-3df9-4bd9-97ba-883a45f0fcf5"), p => p.Item.LastName);

            builder.AddField(Guid.Parse(""), p => p.Item.RSSJobTitle);
            builder.AddField(Guid.Parse(""), p => p.Item.UniversityJobTitle);
            builder.AddField(Guid.Parse(""), p => p.Item.ProfessionalBackground);
            builder.AddField(Guid.Parse(""), p => p.Item.Specialisation);
            builder.AddField(Guid.Parse(""), p => p.Item.StartDateForRSS);
            builder.AddField(Guid.Parse(""), p => p.Item.EndDateForRSS);
            builder.AddField(Guid.Parse(""), p => p.Item.FullTimeEquivalentForRSS);

            builder.AddField(Guid.Parse("d54fa4e2-9062-46f0-bdc3-f42d41818754"), p => p.UserGroup());

            Schema = builder.Schema;
        }

        public static ReportSchema Schema { get; }
    }
}

