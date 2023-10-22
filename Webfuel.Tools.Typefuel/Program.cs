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
                .AnalyseAssembly(schema, typeof(Webfuel.CoreAssemblyMarker).GetTypeInfo().Assembly)
                .AnalyseAssembly(schema, typeof(Webfuel.Common.CommonAssemblyMarker).GetTypeInfo().Assembly)
                .AnalyseAssembly(schema, typeof(Webfuel.Domain.DomainAssemblyMarker).GetTypeInfo().Assembly)
                .AnalyseAssembly(schema, typeof(Webfuel.Domain.StaticData.StaticDataAssemblyMarker).GetTypeInfo().Assembly);

            new MinimalApiAnalyser()
                .AnalyseAssembly(schema, typeof(Webfuel.App.Program).GetTypeInfo().Assembly);

            AngularApiGenerator.GenerateApi(schema);
        }
    }
}