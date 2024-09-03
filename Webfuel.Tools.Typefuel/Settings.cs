using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public static class Settings
    {
        // Solution

        public static string SolutionRoot { get { return @"C:\github\Webfuel.RssIcl"; } }

        // Calculated

        public static string ApiRoot { get { return SolutionRoot + @"\Webfuel.App\ClientApp\src\app\api"; } }
    }
}
