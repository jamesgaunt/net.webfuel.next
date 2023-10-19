using Microsoft.Extensions.Azure;
using Microsoft.Identity.Client;
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
        public static void GenerateService(ApiService service)
        {
            File.WriteAllText(Settings.ApiRoot + $@"\{ServiceFilename(service)}.ts", Controller(service));
        }

        static string Controller(ApiService service)
        {
            var sb = new ScriptBuilder();

            sb.WriteLine("import { EventEmitter, Injectable, inject } from '@angular/core';");
            sb.WriteLine("import { Observable, tap } from 'rxjs';");
            sb.WriteLine("import { ApiService, ApiOptions } from '../core/api.service';");
            sb.WriteLine("import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';");
            sb.WriteLine("import { IDataSource } from 'shared/common/data-source';");

            var complexTypes = EnumerateTypes(service);
            if (complexTypes.Count > 0)
                sb.WriteLine($"import {{ {String.Join(", ", complexTypes.Select(p => AngularTypesGenerator.TypeName(p)))} }} from './api.types';");

            sb.WriteLine();
            sb.WriteLine("@Injectable()");
            sb.Write($"export class {service.Name}Api");

            if (service.DataSource)
            {
                sb.Write($" implements IDataSource<{service.Name}, Query{service.Name},");

                if (service.Methods.Any(p => p.Name == "Create"))
                    sb.Write($" Create{service.Name},");
                else
                    sb.Write($" any,");

                if (service.Methods.Any(p => p.Name == "Update"))
                    sb.Write($" Update{service.Name}>");
                else
                    sb.Write($" any>");
            }

            using (sb.OpenBrace(""))
            {
                sb.WriteLine("constructor(private apiService: ApiService) { }");
                foreach (var action in service.Methods)
                    Action(sb, action);

                if (service.DataSource)
                    sb.WriteLine("\nchanged = new EventEmitter<any>();");

                Resolvers(sb, service);
            }

            return sb.ToString().FormatScript();
        }

        static void Action(ScriptBuilder sb, ApiMethod method)
        {
            sb.WriteLine();
            sb.WriteLine($"public " + method.Name.ToCamelCase() + $" {AngularMethodGenerator.Signature(method)} {{");
            {
                sb.Write($"return this.apiService.request<");

                if (method.BodyParameter != null)
                    AngularTypesGenerator.TypeDescriptor(sb, method.BodyParameter.TypeDescriptor);
                else
                    sb.Write($"undefined");
                sb.Write(", ");
                AngularTypesGenerator.TypeDescriptor(sb, method.ReturnTypeDescriptor);

                sb.Write($">(\"{method.Verb}\", \"{AngularMethodGenerator.RouteUrl(method)}\"");

                if (method.BodyParameter != null)
                    sb.Write($", body");
                else
                    sb.Write($", undefined");

                sb.Write(", options)");

                if (method.Service.DataSource && (method.Name == "Update" || method.Name == "Create" || method.Name == "Delete" || method.Name == "Sort"))
                    sb.Write(".pipe(tap(_ => this.changed.emit()))");

                sb.WriteLine(";");
            }
            sb.WriteLine("}");
        }

        static void Resolvers(ScriptBuilder sb, ApiService service)
        {
            foreach (var method in service.Methods)
            {
                if (method.Name != "Resolve")
                    continue;

                sb.WriteLine();
                sb.WriteLine("// Resolvers");
                sb.WriteLine();
                sb.WriteLine($"static {service.Name.ToCamelCase()}Resolver(param: string): ResolveFn<{service.Name}> {{");
                sb.WriteLine($"return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<{service.Name}> => {{");
                sb.WriteLine($"return inject({service.Name}Api).resolve({{id: route.paramMap.get(param)! }});");
                sb.WriteLine("};");
                sb.WriteLine("}");
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
