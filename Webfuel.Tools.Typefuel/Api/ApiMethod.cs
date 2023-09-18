namespace Webfuel.Tools.Typefuel
{
    public class ApiMethod
    {
        public readonly ApiService Service;

        public ApiMethod(ApiService service)
        {
            Service = service;
        }

        public string Name { get; set; }

        public string Verb { get; set; }

        public ApiRoute Route { get; set; }

        public List<ApiMethodParameter> Parameters { get; } = new List<ApiMethodParameter>();

        public ApiMethodParameter BodyParameter { get => Parameters.FirstOrDefault(p => p.Source == ApiMethodParameterSource.Body); } 

        public IEnumerable<ApiMethodParameter> RouteParameters { get => Parameters.Where(p => p.Source == ApiMethodParameterSource.Route); } 

        public ApiTypeDescriptor ReturnTypeDescriptor { get; set; }

        public void Validate()
        {
            if(BodyParameter != null) {
                if (Verb != "POST" && Verb != "PUT")
                    throw new InvalidOperationException($"{Service.Name}.{Name}: Body parameters can only be specified on POST and PUT methods");
            }

            foreach(var segment in Route.Segments)
            {
                if(segment is ApiRouteParameterSegment routeParameterSegment)
                {
                    if (!RouteParameters.Any(p => p.Name == routeParameterSegment.Name))
                        throw new InvalidOperationException($"{Service.Name}.{Name}: Route parameter {routeParameterSegment.Name} is not a method parameter");
                }
            }
        }

    }

    public class ApiMethodParameter
    {
        public ApiMethod Method;

        public ApiMethodParameter(ApiMethod method)
        {
            Method = method;
        }

        public string Name { get; set; }

        public ApiTypeDescriptor TypeDescriptor { get; set; }

        public ApiMethodParameterSource Source { get; set; }
    }

    public enum ApiMethodParameterSource
    {
        Unknown = 0,
        Body = 1,
        Route = 2,
    }
}
