using Webfuel;

namespace Webfuel.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Webfuel.CoreRegistration.ConfigureServices(builder.Services);
            Webfuel.Common.CommonRegistration.ConfigureServices(builder.Services);
            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
