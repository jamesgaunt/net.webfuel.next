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

            new MvcAnalyser()
                .AnalyseAssembly(schema, typeof(Webfuel.App.Program).GetTypeInfo().Assembly)
                .AnalyseAssembly(schema, typeof(Webfuel.CoreRegistration).GetTypeInfo().Assembly);
            AngularApiGenerator.GenerateApi(schema);
        }
    }
}