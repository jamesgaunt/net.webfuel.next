using System;
using System.Reflection;

namespace Webfuel.Tools.Typefuel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Typefuel");

            var schema = new ApiSchema();

            new TypeAnalyser()
                .AnalyseAssembly(schema, typeof(Webfuel.App.Program).GetTypeInfo().Assembly)
                .AnalyseAssembly(schema, typeof(Webfuel.CoreRegistration).GetTypeInfo().Assembly);

            new MinimalApiAnalyser()
                .AnalyseAssembly(schema, typeof(Webfuel.App.Program).GetTypeInfo().Assembly);

            AngularApiGenerator.GenerateApi(schema);
        }
    }
}