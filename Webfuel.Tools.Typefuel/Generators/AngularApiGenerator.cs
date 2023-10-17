using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Webfuel.Tools.Typefuel
{
    public static class AngularApiGenerator
    {
        public static void GenerateApi(ApiSchema schema)
        {
            if (Directory.Exists(Settings.ApiRoot))
                Directory.Delete(Settings.ApiRoot, true);
            Directory.CreateDirectory(Settings.ApiRoot);

            AngularTypesGenerator.GenerateTypes(schema);
            AngularStaticGenerator.GenerateStatic(schema);
            AngularStaticDataGenerator.GenerateStaticData(schema);
            AngularModuleGenerator.GenerateModule(schema);

            foreach (var controller in schema.Services)
                AngularServiceGenerator.GenerateService(controller);
        }
    }
}
