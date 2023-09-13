using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public class ApiRouteSegment
    {
    }

    public class ApiRouteLiteralSegment : ApiRouteSegment
    {
        public string Text { get; set; }
    }

    public class ApiRouteParameterSegment : ApiRouteSegment
    {
        public string Name { get; set; }

        public string Type { get; set; }

    }

    public class ApiRouteControllerSegment: ApiRouteSegment
    {
    }
}
