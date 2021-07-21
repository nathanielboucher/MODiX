using System.Linq;
using System.Reactive.PlatformServices;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Modix.Bot;
using Modix.Business;
using Modix.Data;
using Modix.Data.Migrations;
using Modix.Host.Logging;
using Modix.Web.Server;

namespace Modix.Host
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddResponseCompression(options => options
                    .MimeTypes = ResponseCompressionDefaults.MimeTypes
                        .Append("application/octet-stream"))
                .AddSingleton<ISystemClock, DefaultSystemClock>()
                .AddLogging(_configuration)
                .Add(services => services.AddHealthChecks()
                    .AddModixData())
                .AddModixBot()
                .AddModixBusiness(_configuration)
                .AddModixData(_configuration)
                .AddModixDataMigrations()
                .AddModixWebServer(_configuration);

        public void Configure(IApplicationBuilder application)
        {
            application
                .UseResponseCompression();

            if (_webHostEnvironment.IsDevelopment())
                application
                    .UseDeveloperExceptionPage()
                    .UseWebAssemblyDebugging();

            application
                .UseHttpsRedirection()
                .UseBlazorFrameworkFiles()
                .UseStaticFiles()
                .UseRouting()
                .UseEndpoints(builder => builder
                    .MapHealthChecks("/health"))
                .UseModixWebServer()
                .UseFallbackFile("index.html");
        }

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
    }
}
