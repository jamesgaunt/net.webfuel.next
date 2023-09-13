using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Datafuel
{
    public static class Settings
    {
        // Database

        public static string DatabaseName { get { return "net.webfuel.next"; } }

        public static string DatabaseSchema { get { return "next"; } }

        public static string DatabaseServer { get { return "Data Source=JAMES-RTX\\SQLEXPRESS;integrated Security=true"; } }

        // Solution

        public static string SolutionRoot { get { return @"C:\Repo\net.webfuel.next"; } }

        // Calculated

        public static string DefinitionRoot { get { return SolutionRoot + @"\Webfuel.Tools.Datafuel\Definition"; } }
    }
}
