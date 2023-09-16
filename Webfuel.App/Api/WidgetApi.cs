using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading;

namespace Webfuel.App
{
    public class WidgetApi: ITypefuelApi
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapCommand<CreateWidgetCommand>();
            app.MapCommand<UpdateWidgetCommand>();
            app.MapCommand<DeleteWidgetCommand>();
            app.MapCommand<QueryWidgetCommand>();
        }
    }
}
