using Microsoft.Extensions.Azure;
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

            sb.WriteLine("import { EventEmitter, Injectable, inject } from '@angular/core';");
            sb.WriteLine("import { Observable } from 'rxjs';");
            sb.WriteLine("import { ApiService, ApiOptions } from '../core/api.service';");
            sb.WriteLine("import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';");
            sb.WriteLine("import { IDataSource } from '../shared/data-source/data-source';");

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

                Resolvers(sb, controller);
                DataSources(sb, controller);
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

        static void Resolvers(ScriptBuilder sb, ApiService service)
        {
            foreach (var method in service.Methods)
            {
                if (method.Name.StartsWith("Resolve"))
                {
                    var resolveType = method.Name.Replace("Resolve", "");
                    if (resolveType == service.Name)
                    {
                        sb.WriteLine();
                        sb.WriteLine("// Resolvers");
                        sb.WriteLine();
                        sb.WriteLine($"static {resolveType.ToCamelCase()}Resolver(param: string): ResolveFn<{resolveType}> {{");
                        sb.WriteLine($"return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<{resolveType}> => {{");
                        sb.WriteLine($"return inject({resolveType}Api).resolve{resolveType}({{id: route.paramMap.get(param)! }});");
                        sb.WriteLine("};");
                        sb.WriteLine("}");
                    }
                }
            }
        }

        static void DataSources(ScriptBuilder sb, ApiService service)
        {
            foreach (var method in service.Methods)
            {
                if (method.Name.StartsWith("Query"))
                {
                    var dataSourceType = method.Name.Replace("Query", "");
                    if (dataSourceType == service.Name)
                    {
                        sb.WriteLine();
                        sb.WriteLine("// Data Sources");
                        sb.WriteLine();
                        sb.WriteLine($"{dataSourceType.ToCamelCase()}DataSource: IDataSource<{dataSourceType}> = {{");
                        sb.WriteLine($"fetch: (query) => this.query{dataSourceType}(query),");
                        sb.WriteLine($"changed: new EventEmitter()");
                        sb.WriteLine("}");
                    }
                }
            }
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
