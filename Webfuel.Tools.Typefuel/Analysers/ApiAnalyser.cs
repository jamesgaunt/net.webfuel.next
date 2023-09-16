using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Webfuel.Tools.Typefuel
{
    public class ApiAnalyser
    {
        public ApiAnalyser AnalyseAssembly(ApiSchema schema, Assembly assembly)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                if (type.ImplementedInterfaces.Contains(typeof(ITypefuelApi)))
                    AnalyseApi(schema, type);
            }

            return this;
        }

        public void AnalyseApi(ApiSchema schema, Type apiType)
        {
            var instance = Activator.CreateInstance(apiType) as ITypefuelApi;
            if (instance == null)
                throw new InvalidOperationException("Unable to instantiate ITypefuelApi");

            var extractor = new TypefuelApiExtractor();
            instance.RegisterEndpoints(extractor);
            if (extractor.Commands.Count == 0)
                return;

            var controller = new ApiController(schema);
            controller.Name = apiType.Name.Replace("Api", "");

            foreach (var command in extractor.Commands)
                AnalyseCommand(controller, command);

            schema.Controllers.Add(controller);
        }

        public void AnalyseCommand(ApiController controller, TypefuelApiCommand command)
        {
            var action = new ApiAction(controller);

            action.Name = command.CommandType.Name.Replace("Command", "");
            action.Verb = "COMMAND";
            action.Route = AnalyseRoute(action, command);
            action.ReturnTypeDescriptor = controller.Schema.TypeContext.GetTypeDescriptor(GetReturnType(command));
            action.CommandTypeDescriptor = controller.Schema.TypeContext.GetTypeDescriptor(command.CommandType);

            if (!ValidReturnType(action.ReturnTypeDescriptor))
                throw new InvalidOperationException($"Invalid Command {command.CommandType.Name}: API actions must return JSON");

            controller.Actions.Add(action);
        }

        public ApiRoute AnalyseRoute(ApiAction action, TypefuelApiCommand command)
        {
            var route = new ApiRoute(action);
            route.Segments.Add(new ApiRouteLiteralSegment { Text = command.Pattern });
            return route;
        }

        public Type GetReturnType(TypefuelApiCommand command)
        {
            foreach (var interfaceType in command.CommandType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition()
                    == typeof(IRequest<>))
                {
                    return interfaceType.GetGenericArguments()[0];
                }
            }

            return typeof(void);
        }

        public bool ValidReturnType(ApiTypeDescriptor typeDescriptor)
        {
            if (typeDescriptor.Type is ApiComplexType)
                return true;

            if (typeDescriptor.Type is ApiPrimativeType && (typeDescriptor.Type as ApiPrimativeType).TypeCode == ApiTypeCode.Void)
                return true;

            return false;
        }
    }
}
