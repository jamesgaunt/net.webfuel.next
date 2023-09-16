using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Webfuel.Tools.Typefuel
{
    public static class AngularActionGenerator
    {
        public static string FixReservedNames(string name)
        {
            if (name == "function")
                return "_" + name;
            return name;
        }

        public static string Signature(ApiAction action)
        {
            var sb = new ScriptBuilder();

            sb.Write("(");
            
            if (action.Parameters.Count > 0)
            {
                sb.Write("params: { ");
                for (int i = 0; i < action.Parameters.Count; i++)
                {
                    if (i > 0)
                        sb.Write(", ");

                    var parameter = action.Parameters[i];

                    sb.Write(FixReservedNames(parameter.Name.ToCamelCase()));

                    // if (parameter.HasDefaultValue)
                    //    output.Append("?");

                    sb.Write(": ");
                    AngularTypesGenerator.TypeDescriptor(sb, parameter.TypeDescriptor);
                }
                sb.Write(" }, ");
            }
            else if(action.CommandTypeDescriptor != null)
            {
                sb.Write("command: ");
                AngularTypesGenerator.TypeDescriptor(sb, action.CommandTypeDescriptor);
                sb.Write(", ");
            }
            
            sb.Write("options?: ApiOptions): Observable<");
            AngularTypesGenerator.TypeDescriptor(sb, action.ReturnTypeDescriptor);
            sb.Write(">");

            return sb.ToString();
        }

        public static string Map(ApiAction action)
        {
            if(action.ReturnTypeDescriptor.Type is ApiComplexType)
                return $".pipe(map((res) => <{AngularTypesGenerator.TypeDescriptor(action.ReturnTypeDescriptor)}>res.body))";

            if(action.ReturnTypeDescriptor.Type is ApiPrimativeType)
            {
                var primativeReturnType = action.ReturnTypeDescriptor.Type as ApiPrimativeType;

                switch (primativeReturnType.TypeCode)
                {
                    case ApiTypeCode.Void:
                        return String.Empty;
                }
            }

            throw new InvalidOperationException("Unrecognised api return type");
        }

        public static string RouteUrl(ApiAction action)
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
                    sb.Write(action.Controller.Name);
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
