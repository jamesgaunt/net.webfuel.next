using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public static class IdentityStatic
    {
        public static string StateKey { get; } = "IDENTITY_STATE_KEY";

        public static string Header { get; } = "identity-token";
    }
}
