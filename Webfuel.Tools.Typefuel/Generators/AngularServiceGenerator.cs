using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Webfuel.Tools.Typefuel
{
    public static class AngularServiceGenerator
    {
        public static void GenerateService(ApiService controller)
        {
            File.WriteAllText(Settings.ApiRoot + $@"\{ServiceFilename(controller)}.ts", Controller(controller));
        }

        static string Controller(ApiService controller)
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
                foreach (var action in controller.Methods)
                    Action(sb, action);
            }

            return sb.ToString().FormatScript();
        }

        static void Action(ScriptBuilder sb, ApiMethod action)
        {
            sb.WriteLine();
            sb.WriteLine($"public " + action.Name.ToCamelCase() + $" {AngularMethodGenerator.Signature(action)} {{");
            {
                sb.Write($"return this.apiService.request(\"{action.Verb}\", \"{AngularMethodGenerator.RouteUrl(action)}\"");

                if (action.BodyParameter != null)
                    sb.Write($", body");
                else
                    sb.Write($", undefined");

                sb.WriteLine(", options);"); 
            }
            sb.WriteLine("}");
        }

        public static string ServiceFilename(ApiService service)
        {
            return Regex.Replace(service.Name, @"([a-z])([A-Z])", "$1-$2").ToLower() + ".api";
        }

        static List<ApiComplexType> EnumerateTypes(ApiService controller)
        {
            var result = new List<ApiComplexType>();

            foreach (var action in controller.Methods)
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
            }
            return result;
        }
    }
}
