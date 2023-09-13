using System;
using System.Collections.Generic;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public class ApiRoute
    {
        public readonly ApiAction Action;

        public ApiRoute(ApiAction action)
        {
            Action = action;
        }

        public List<ApiRouteSegment> Segments { get; } = new List<ApiRouteSegment>();
    }
}
