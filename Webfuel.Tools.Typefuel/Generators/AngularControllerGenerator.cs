using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Webfuel.Tools.Typefuel
{
    public static class AngularControllerGenerator
    {
        public static void GenerateController(ApiController controller)
        {
            File.WriteAllText(Settings.ApiRoot + $@"\{ControllerFilename(controller)}.ts", Controller(controller));
        }

        static string Controller(ApiController controller)
        {
            var sb = new ScriptBuilder();

            sb.WriteLine("import { Injectable } from '@angular/core';");
            sb.WriteLine("import { Observable } from 'rxjs';");
            sb.WriteLine("import { map } from 'rxjs/operators';");
            sb.WriteLine("import { ApiService, ApiOptions } from '../core/api.service';");

            var complexTypes = EnumerateTypes(controller);
            if (complexTypes.Count > 0)
                sb.WriteLine($"import {{ {String.Join(", ", complexTypes.Select(p => AngularTypesGenerator.TypeName(p)))} }} from './api.types';");

            sb.WriteLine();
            sb.WriteLine("@Injectable()");

            using (sb.OpenBrace("export class " + controller.Name + "Api"))
            {
                sb.WriteLine("constructor(private apiService: ApiService) { }");
                foreach (var action in controller.Actions)
                    Action(sb, action);
            }

            return sb.ToString().FormatScript();
        }

        static void Action(ScriptBuilder sb, ApiAction action)
        {
            sb.WriteLine();
            sb.WriteLine($"public " + action.Name.ToCamelCase() + $" {AngularActionGenerator.Signature(action)} {{");
            {
                //sb.WriteLine("options = options || {};");
                //if (action.RetryCount >= 0)
                //    sb.WriteLine($"options.retryCount = options.retryCount || {action.RetryCount};");

                sb.Write($"return this.apiService.{action.Verb}(\"{AngularActionGenerator.RouteUrl(action)}\"");

                if (action.Verb == "POST" || action.Verb == "PUT" || action.Verb == "COMMAND")
                {
                    if (action.BodyParameter != null)
                        sb.Write($", params.{AngularActionGenerator.FixReservedNames(action.BodyParameter.Name)}");
                    else if (action.CommandTypeDescriptor != null)
                        sb.Write($", command");
                    else
                        sb.Write($", undefined");
                }

                sb.WriteLine(", options)" + AngularActionGenerator.Map(action) + ";");
            }
            sb.WriteLine("}");
        }

        public static string ControllerFilename(ApiController controller)
        {
            return Regex.Replace(controller.Name, @"([a-z])([A-Z])", "$1-$2").ToLower() + ".api";
        }

        static List<ApiComplexType> EnumerateTypes(ApiController controller)
        {
            var result = new List<ApiComplexType>();

            foreach (var action in controller.Actions)
            {
                foreach (var parameter in action.Parameters)
                {
                    foreach (var type in parameter.TypeDescriptor.EnumerateTypes())
                    {
                        if (!result.Contains(type) && type is ApiComplexType)
                            result.Add(type as ApiComplexType);
                    }
                }

                foreach (var type in action.ReturnTypeDescriptor.EnumerateTypes())
                {
                    if (!result.Contains(type) && type is ApiComplexType)
                        result.Add(type as ApiComplexType);
                }

                if (action.CommandTypeDescriptor != null)
                {
                    foreach (var type in action.CommandTypeDescriptor.EnumerateTypes())
                    {
                        if (!result.Contains(type) && type is ApiComplexType)
                            result.Add(type as ApiComplexType);
                    }
                }

            }
            return result;
        }
    }
}
