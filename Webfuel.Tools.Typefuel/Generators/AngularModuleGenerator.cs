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

            foreach (var controller in schema.Controllers)
            {
                sb.WriteLine($"import {{ {controller.Name}Api }} from './{AngularControllerGenerator.ControllerFilename(controller)}';");
            }
            sb.WriteLine();
            sb.WriteLine("@NgModule({");
            sb.WriteLine("\tproviders: [");
            for (var i = 0; i < schema.Controllers.Count; i++)
            {
                sb.WriteLine(schema.Controllers[i].Name + "Api" + (i < schema.Controllers.Count - 1 ? "," : ""));
            }
            sb.WriteLine("\t]");
            sb.WriteLine("})");
            sb.WriteLine("export class ApiModule { }");

            return sb.ToString().FormatScript();
        }
    }
}
