using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Webfuel.Tools.Typefuel
{
    public class TypeAnalyser
    {
        public TypeAnalyser AnalyseAssembly(ApiSchema schema, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                var typeInfo = type.GetTypeInfo();

                // Detect Static Classes
                if (typeInfo.IsClass && typeInfo.GetCustomAttribute<TypefuelStaticAttribute>() != null)
                    schema.AnalyseStatic(type);

                // Detect Static Enums
                if (typeInfo.IsEnum && typeInfo.GetCustomAttribute<TypefuelStaticAttribute>() != null)
                    schema.AnalyseStatic(type);

                // Detect Interfaces
                if (typeInfo.IsClass && typeInfo.GetCustomAttribute<TypefuelInterfaceAttribute>() != null)
                    schema.TypeContext.GetTypeDescriptor(type);
            }

            return this;
        }
    }
}
