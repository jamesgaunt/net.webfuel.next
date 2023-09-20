using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public class Tenant
    {
        public required string Name { get; set; }

        public required string DatabaseSchema { get; set; }
    }

    public static class Settings
    {
        // Database

        public static string DatabaseName { get { return "net.webfuel.next"; } }

        public static string DatabaseServer { get { return "Data Source=JAMES-RTX\\SQLEXPRESS;Integrated Security=true;TrustServerCertificate=true"; } }

        // Tenants

        public static List<Tenant> Tenants { get; } = new List<Tenant>
        {
            new Tenant { Name = "Development", DatabaseSchema = "rssdev" },
            new Tenant { Name = "RSSLeicester", DatabaseSchema = "rsslct" },
            new Tenant { Name = "RSSLondon", DatabaseSchema = "rssldn" }
        };

        // Solution

        public static string SolutionRoot { get { return @"C:\Repo\net.webfuel.next"; } }

        // Calculated

        public static string DefinitionRoot { get { return SolutionRoot + @"\Webfuel.Tools.Datafuel\Definition"; } }
    }
}
