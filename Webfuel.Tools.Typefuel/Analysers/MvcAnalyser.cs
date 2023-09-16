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
    public class MvcAnalyser
    {
        public MvcAnalyser AnalyseAssembly(ApiSchema schema, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                var typeInfo = type.GetTypeInfo();

                // Detect Controllers
                if (typeInfo.IsClass && typeInfo.GetCustomAttribute<TypefuelControllerAttribute>() != null)
                    schema.Controllers.Add(AnalyseController(schema, type));
            }

            return this;
        }

        public ApiController AnalyseController(ApiSchema schema, Type controllerType)
        {
            var controller = new ApiController(schema);

            controller.Name = controllerType.Name.Replace("Controller", "");

            // Detect Actions
            foreach (var method in controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (method.GetCustomAttribute<HttpMethodAttribute>() == null || method.GetCustomAttribute<TypefuelIgnoreAttribute>() != null)
                    continue;

                controller.Actions.Add(AnalyseAction(controller, method));
            }

            return controller;
        }

        public ApiAction AnalyseAction(ApiController controller, MethodInfo methodInfo)
        {
            var action = new ApiAction(controller);

            action.Name = methodInfo.Name;
            action.Verb = GetActionMethodVerb(methodInfo);
            action.Route = AnalyseRoute(action, methodInfo);
            action.ReturnTypeDescriptor = controller.Schema.TypeContext.GetTypeDescriptor(methodInfo.ReturnType);

            var typefuelActionAttribute = methodInfo.GetCustomAttribute<TypefuelActionAttribute>();
            if (typefuelActionAttribute != null)
            {
                action.RetryCount = typefuelActionAttribute.RetryCount;
            }

            if (!ValidReturnType(action.ReturnTypeDescriptor))
                throw new InvalidOperationException($"Invalid Action {methodInfo.DeclaringType.Name}.{methodInfo.Name}: API actions must return JSON");

            foreach (var parameterInfo in methodInfo.GetParameters())
            {
                if (parameterInfo.GetCustomAttribute<TypefuelIgnoreAttribute>() != null)
                    continue;
                if (parameterInfo.GetCustomAttributes().Any(p => (p is IBindingSourceMetadata) && !(p is FromBodyAttribute) && !(p is FromRouteAttribute)))
                    continue;
                action.Parameters.Add(AnalyseActionParameter(action, parameterInfo));
            }

            return action;
        }

        public bool ValidReturnType(ApiTypeDescriptor typeDescriptor)
        {
            if (typeDescriptor.Type is ApiComplexType)
                return true;

            if (typeDescriptor.Type is ApiPrimativeType && (typeDescriptor.Type as ApiPrimativeType).TypeCode == ApiTypeCode.Void)
                return true;

            return false;
        }

        public ApiActionParameter AnalyseActionParameter(ApiAction action, ParameterInfo parameterInfo)
        {
            var actionParameter = new ApiActionParameter(action);

            actionParameter.Name = parameterInfo.Name;
            actionParameter.TypeDescriptor = action.Controller.Schema.TypeContext.GetTypeDescriptor(parameterInfo.ParameterType);

            if (parameterInfo.GetCustomAttribute<FromBodyAttribute>() != null)
                actionParameter.Source = ApiActionParameterSource.Body;
            else
                actionParameter.Source = ApiActionParameterSource.Route;

            return actionParameter;
        }

        public ApiRoute AnalyseRoute(ApiAction action, MethodInfo methodInfo)
        {
            var route = new ApiRoute(action);

            var routeTemplate = GetActionRouteTemplate(methodInfo);
            var routeSegmentParts = routeTemplate.Split('/');

            foreach (var routeSegmentPart in routeSegmentParts)
            {
                if (String.IsNullOrEmpty(routeSegmentPart))
                    throw new InvalidOperationException($"Invalid Route {methodInfo.DeclaringType.Name}.{methodInfo.Name}: Contains empty segment");

                if (routeSegmentPart.StartsWith("{"))
                {
                    route.Segments.Add(ParseRouteParameter(routeSegmentPart));
                }
                else if (routeSegmentPart == "[controller]")
                {
                    route.Segments.Add(new ApiRouteControllerSegment());
                }
                else
                {
                    route.Segments.Add(new ApiRouteLiteralSegment { Text = routeSegmentPart });
                }
            }

            return route;
        }

        public ApiRouteParameterSegment ParseRouteParameter(string routeSegmentPart)
        {
            if (String.IsNullOrEmpty(routeSegmentPart) || !routeSegmentPart.StartsWith("{") || !routeSegmentPart.EndsWith("}") || routeSegmentPart.Length < 3)
                throw new InvalidOperationException($"Invalid Route Parameter: {routeSegmentPart}");

            // Strip opening and closing brace
            routeSegmentPart = routeSegmentPart.Substring(1, routeSegmentPart.Length - 2);

            // NOTE: We don't support optional parameters or default parameter values (yet)

            var parts = routeSegmentPart.Split(':');
            if (parts.Length != 2)
                throw new InvalidOperationException($"Invalid Route Parameter: {routeSegmentPart}");

            var parameterName = parts[0];
            var parameterType = parts[1];

            if (!parameterName.IsValidIdentifier())
                throw new InvalidOperationException($"Invalid Route Parameer: {routeSegmentPart}");

            return new ApiRouteParameterSegment { Name = parameterName, Type = parameterType };
        }

        public string GetActionRouteTemplate(MethodInfo methodInfo)
        {
            var actionRouteAttribute = methodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;
            var controllerRouteAttribute = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttribute(typeof(RouteAttribute)) as RouteAttribute;

            if (actionRouteAttribute == null)
                throw new InvalidOperationException($"Invalid Route {methodInfo.DeclaringType.Name}.{methodInfo.Name}: Missing HttpMethodAttribute");

            var routeTemplate = actionRouteAttribute.Template;
            if (routeTemplate == null)
                throw new InvalidOperationException($"Invalid Route {methodInfo.DeclaringType.Name}.{methodInfo.Name}: HttpMethodAttribute has no route template");

            if (routeTemplate.StartsWith("~"))
            {
                // Route template is absolute
                if (!routeTemplate.StartsWith("~/"))
                    throw new InvalidOperationException($"Invalid Route {methodInfo.DeclaringType.Name}.{methodInfo.Name}: Template is missing initial ~/");
                routeTemplate = routeTemplate.Substring(2);
            }
            else
            {
                // Combine action and controller templates
                if (controllerRouteAttribute != null && !String.IsNullOrEmpty(controllerRouteAttribute.Template))
                    routeTemplate = controllerRouteAttribute.Template + (String.IsNullOrEmpty(routeTemplate) ? "" : "/" + routeTemplate);
            }

            routeTemplate = routeTemplate.Trim();

            if (String.IsNullOrEmpty(routeTemplate) || routeTemplate.Contains(" "))
                throw new InvalidOperationException($"Invalid Route {methodInfo.DeclaringType.Name}.{methodInfo.Name}: Template is empty or contains whitespace");

            if (routeTemplate.Contains("?"))
                throw new InvalidOperationException($"Invalid Route {methodInfo.DeclaringType.Name}.{methodInfo.Name}: Optional parameters are not supported");

            if (routeTemplate.Contains("="))
                throw new InvalidOperationException($"Invalid Route {methodInfo.DeclaringType.Name}.{methodInfo.Name}: Default parameter values are not supported");

            return routeTemplate;
        }

        public string GetActionMethodVerb(MethodInfo methodInfo)
        {
            // Configuration
            if (methodInfo.GetCustomAttribute(typeof(HttpGetAttribute)) != null)
                return "GET";
            if (methodInfo.GetCustomAttribute(typeof(HttpPostAttribute)) != null)
                return "POST";
            if (methodInfo.GetCustomAttribute(typeof(HttpPutAttribute)) != null)
                return "PUT";
            if (methodInfo.GetCustomAttribute(typeof(HttpDeleteAttribute)) != null)
                return "DELETE";

            // Convention
            if (methodInfo.Name.StartsWith("Get", StringComparison.OrdinalIgnoreCase))
                return "GET";
            if (methodInfo.Name.StartsWith("Post", StringComparison.OrdinalIgnoreCase))
                return "POST";
            if (methodInfo.Name.StartsWith("Put", StringComparison.OrdinalIgnoreCase))
                return "PUT";
            if (methodInfo.Name.StartsWith("Delete", StringComparison.OrdinalIgnoreCase))
                return "DELETE";

            // Default
            return "POST";
        }
    }
}
