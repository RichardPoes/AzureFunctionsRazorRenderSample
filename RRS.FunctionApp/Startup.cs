using System.IO;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.ObjectPool;
using RRS.FunctionApp;

[assembly: FunctionsStartup(typeof(Startup))]
namespace RRS.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var compiledViewAssembly = Assembly.LoadFile(Path.Combine(executionPath, "RRS.FunctionApp.Views.dll"));
            var services = builder.Services;
            services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
            services.AddScoped<IRazorViewRenderer, RazorViewRenderer>();
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddMvcCore()
                    .AddViews()
                    .AddRazorViewEngine()
                    .AddApplicationPart(compiledViewAssembly);
        }
    }
}
