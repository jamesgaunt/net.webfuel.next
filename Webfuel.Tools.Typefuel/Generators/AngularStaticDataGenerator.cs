using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public static class AngularStaticDataGenerator
    {
        public static void GenerateStaticData(ApiSchema schema)
        {
            File.WriteAllText(Settings.ApiRoot + $@"\static-data.cache.ts", StaticDataCache(schema));
        }

        static string StaticDataCache(ApiSchema schema)
        {
            var sb = new ScriptBuilder();

            sb.WriteLine("import { Injectable } from '@angular/core';");
            sb.WriteLine("import { IDataSource, IDataSourceWithGet } from 'shared/common/data-source';");
            sb.WriteLine("import { StaticDataService } from '../core/static-data.service';");

            var staticDataServices = schema.Services.Where(p => p.StaticData).ToList();
            if (staticDataServices.Count > 0)
                sb.WriteLine($"import {{ {String.Join(", ", staticDataServices.Select(p => p.Name.Replace("Api", "")))} }} from './api.types';");

            sb.WriteLine();

            sb.WriteLine("@Injectable()");
            using (sb.OpenBrace("export class StaticDataCache"))
            {
                sb.WriteLine();

                using (sb.OpenBrace("constructor(private staticDataService: StaticDataService)"))
                {
                }

                foreach (var staticDataService in staticDataServices)
                {
                    var n = staticDataService.Name.Replace("Api", "");
                    var c = n.ToCamelCase();

                    sb.WriteLine();
                    sb.WriteLine($"{c}: IDataSourceWithGet<{n}> = {{");
                    sb.WriteLine($"query: (query) => this.staticDataService.queryFactory(query, s => s.{c}),");
                    sb.WriteLine($"get: (params: {{ id: string }}) => this.staticDataService.getFactory(params.id, s => s.{c}),");
                    sb.WriteLine("};");
                }

            }
            return sb.ToString().FormatScript();
        }

    }
}
