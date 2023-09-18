using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public static class AngularModuleGenerator
    {
        public static void GenerateModule(ApiSchema schema)
        {
            File.WriteAllText(Settings.ApiRoot + $@"\api.module.ts", Module(schema));
        }

        public static string Module(ApiSchema schema)
        {
            var sb = new ScriptBuilder();

            sb.WriteLine("import { NgModule } from '@angular/core';");

            foreach (var service in schema.Services)
            {
                sb.WriteLine($"import {{ {service.Name}Api }} from './{AngularServiceGenerator.ServiceFilename(service)}';");
            }
            sb.WriteLine();
            sb.WriteLine("@NgModule({");
            sb.WriteLine("\tproviders: [");
            for (var i = 0; i < schema.Services.Count; i++)
            {
                sb.WriteLine(schema.Services[i].Name + "Api" + (i < schema.Services.Count - 1 ? "," : ""));
            }
            sb.WriteLine("\t]");
            sb.WriteLine("})");
            sb.WriteLine("export class ApiModule { }");

            return sb.ToString().FormatScript();
        }
    }
}
