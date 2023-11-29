using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Webfuel.Scribble
{
    internal class ScribbleTypeInfo
    {
        internal readonly Type Type;
        internal readonly TypeInfo TypeInfo;

        public ScribbleTypeInfo(Type type)
        {
            Type = type;
            TypeInfo = type.GetTypeInfo();

            Name = Type.Name;
        }

        public string Name { get; }

        public bool IsGenericTypeDefinition
        {
            get { return Type.IsGenericTypeDefinition; }
        }

        public int GenericTypeParameterCount
        {
            get { return TypeInfo.GetGenericArguments().Length; }
        }

        public ScribbleTypeInfo MakeGenericType(IEnumerable<ScribbleTypeName> typeNames)
        {
            return Reflection.GetTypeInfo(Type.MakeGenericType(Reflection.MapConstructableTypes(typeNames).ToArray()));
        }

        public IReadOnlyList<ScribbleConstructorInfo> Constructors
        {
            get
            {
                if (_Constructors == null)
                {
                    var temp = new List<ScribbleConstructorInfo>();
                    foreach (var constructor in TypeInfo.GetConstructors(BindingFlags.Instance | BindingFlags.Public))
                    {
                        temp.Add(new ScribbleConstructorInfo(constructor));
                    }
                    _Constructors = temp;
                }
                return _Constructors;
            }
        }
        private IReadOnlyList<ScribbleConstructorInfo>? _Constructors = null;

        public IReadOnlyList<ScribbleMethodInfo> Methods
        {
            get
            {
                if (_Methods == null)
                {
                    var temp = new List<ScribbleMethodInfo>();
                    foreach (var method in TypeInfo.GetMethods(BindingFlags.Instance | BindingFlags.Public))
                    {
                        if (IsIgnoredMethod(method))
                            continue;
                        temp.Add(new ScribbleMethodInfo(method, extensionMethod: false));
                    }
                    _Methods = temp;
                }
                return _Methods;
            }
        }
        private IReadOnlyList<ScribbleMethodInfo>? _Methods = null;

        public IReadOnlyList<ScribbleMethodInfo> InterfaceMethods
        {
            get
            {
                if (_InterfaceMethods == null)
                {
                    var temp = new List<ScribbleMethodInfo>();
                    foreach (var @interface in TypeInfo.ImplementedInterfaces)
                    {
                        foreach (var method in @interface.GetMethods())
                        {
                            if (IsIgnoredMethod(method) || method.IsAbstract)
                                continue;
                            temp.Add(new ScribbleMethodInfo(method, extensionMethod: false));
                        }
                    }
                    _InterfaceMethods = temp;
                }
                return _InterfaceMethods;
            }
        }
        private IReadOnlyList<ScribbleMethodInfo>? _InterfaceMethods = null;

        public IReadOnlyList<ScribbleMethodInfo> GetExtensionMethods(string name)
        {
            if (_ExtensionMethods.ContainsKey(name))
                return _ExtensionMethods[name];

            var temp = new List<ScribbleMethodInfo>();
            var methods = Reflection.GetExtensionMethods(Type, name);
            foreach (var method in methods)
            {
                if (IsIgnoredMethod(method) || method.IsAbstract)
                    continue;
                temp.Add(new ScribbleMethodInfo(method, extensionMethod: true));
            }
            return _ExtensionMethods[name] = temp;
        }
        private ConcurrentDictionary<string, IReadOnlyList<ScribbleMethodInfo>> _ExtensionMethods = new ConcurrentDictionary<string, IReadOnlyList<ScribbleMethodInfo>>();

        public IReadOnlyList<ScribblePropertyInfo> Properties
        {
            get
            {
                if (_Properties == null)
                {
                    var properties = TypeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.GetIndexParameters().Length == 0).Select(p => new ScribblePropertyInfo(p)).ToList();

                    // Iterate over interfaces to add additional properties
                    foreach (var @interface in TypeInfo.ImplementedInterfaces)
                    {
                        foreach (var property in @interface.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                        {
                            if (properties.Any(p => p.Name == property.Name))
                                continue;
                            properties.Add(new ScribblePropertyInfo(property));
                        }
                    }

                    _Properties = properties;
                }
                return _Properties;
            }
        }
        private IReadOnlyList<ScribblePropertyInfo>? _Properties = null;

        public IReadOnlyList<ScribbleIndexInfo> Indexes
        {
            get
            {
                if (_Indexes == null)
                {
                    var indexers = TypeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.GetIndexParameters().Length > 0).Select(p => new ScribbleIndexInfo(p)).ToList();

                    _Indexes = indexers;
                }
                return _Indexes;
            }
        }
        private IReadOnlyList<ScribbleIndexInfo>? _Indexes = null;

        // Ignored Methods

        private bool IsIgnoredMethod(MethodInfo method)
        {
            if (method.Name.StartsWith("__"))
                return true;
            if (method.Name == "GetType" || method.Name == "GetRawData")
                return true;
            return false;
        }
    }
}