using Microsoft.AspNetCore.Routing.Template;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public class ApiRoute
    {
        public ApiRoute()
        {
        }

        public List<ApiRouteSegment> Segments { get; } = new List<ApiRouteSegment>();

        public static ApiRoute Parse(string pattern)
        {
            var route = new ApiRoute();
            var routeSegmentParts = pattern.Split('/');

            foreach (var routeSegmentPart in routeSegmentParts)
            {
                if (String.IsNullOrEmpty(routeSegmentPart))
                    throw new InvalidOperationException($"Invalid route patter '{pattern}' contains empty segment");

                if (routeSegmentPart.StartsWith("{"))
                {
                    route.Segments.Add(ParseRouteParameter(routeSegmentPart));
                }
                else if (routeSegmentPart == "[controller]")
                {
                    route.Segments.Add(new ApiRouteControllerSegment());
                }
                else
                {
                    route.Segments.Add(new ApiRouteLiteralSegment { Text = routeSegmentPart });
                }
            }

            return route;
        }

        static ApiRouteParameterSegment ParseRouteParameter(string routeSegmentPart)
        {
            if (String.IsNullOrEmpty(routeSegmentPart) || !routeSegmentPart.StartsWith("{") || !routeSegmentPart.EndsWith("}") || routeSegmentPart.Length < 3)
                throw new InvalidOperationException($"Invalid route parameter: {routeSegmentPart}");

            // Strip opening and closing brace
            routeSegmentPart = routeSegmentPart.Substring(1, routeSegmentPart.Length - 2);

            // NOTE: We don't support optional parameters or default parameter values (yet)

            var parts = routeSegmentPart.Split(':');
            if (parts.Length != 2)
                throw new InvalidOperationException($"Invalid route parameter: {routeSegmentPart}");

            var parameterName = parts[0];
            var parameterType = parts[1];

            if (!parameterName.IsValidIdentifier())
                throw new InvalidOperationException($"Invalid route parameer: {routeSegmentPart}");

            return new ApiRouteParameterSegment { Name = parameterName, Type = parameterType };
        }
    }

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

    public class ApiRouteControllerSegment : ApiRouteSegment
    {
    }
}
