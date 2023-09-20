using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;

namespace Webfuel.Tools.Typefuel
{
    public static class AngularMethodGenerator
    {
        public static string FixReservedNames(string name)
        {
            if (name == "function")
                return "_" + name;
            return name;
        }

        public static string Signature(ApiMethod method)
        {
            method.Validate();

            var sb = new ScriptBuilder();

            sb.Write("(");
            
            if(method.BodyParameter != null)
            {
                sb.Write("body: ");
                AngularTypesGenerator.TypeDescriptor(sb, method.BodyParameter.TypeDescriptor);
                sb.Write(", ");
            }

            if (method.RouteParameters.Count() > 0)
            {
                sb.Write("params: { ");
                for (int i = 0; i < method.RouteParameters.Count(); i++)
                {
                    if (i > 0)
                        sb.Write(", ");

                    var parameter = method.RouteParameters.ElementAt(i);

                    sb.Write(FixReservedNames(parameter.Name.ToCamelCase()));

                    // if (parameter.HasDefaultValue)
                    //    output.Append("?");

                    sb.Write(": ");
                    AngularTypesGenerator.TypeDescriptor(sb, parameter.TypeDescriptor);
                }
                sb.Write(" }, ");
            }
            
            sb.Write("options?: ApiOptions): Observable<");
            AngularTypesGenerator.TypeDescriptor(sb, method.ReturnTypeDescriptor);

            sb.Write(">");

            return sb.ToString();
        }

        public static string RouteUrl(ApiMethod action)
        {
            var sb = new ScriptBuilder();

            for(var i = 0; i < action.Route.Segments.Count; i++)
            {
                var segment = action.Route.Segments[i];

                if(segment is ApiRouteLiteralSegment)
                {
                    if (i > 0)
                        sb.Write("/");
                    sb.Write((segment as ApiRouteLiteralSegment).Text);
                }
                else if(segment is ApiRouteControllerSegment)
                {
                    if (i > 0)
                        sb.Write("/");
                    sb.Write(action.Service.Name);
                }
                else if(segment is ApiRouteParameterSegment)
                {
                    var parameter = segment as ApiRouteParameterSegment;

                    if (i > 0)
                        sb.Write("/");
                    sb.Write("\" + params." + parameter.Name + " + \"");
                }
                else
                {
                    throw new InvalidOperationException("Unrecognised route segment type");
                }
            }

            return sb.ToString();
        }

    }
}
