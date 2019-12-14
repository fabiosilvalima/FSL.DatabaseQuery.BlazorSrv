using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FSL.Framework.Web.Extensions;
using FSL.DatabaseQuery.Core.Repository;
using FSL.Framework.Core.Service;
using FSL.DatabaseQuery.Core.Models;
using FSL.Framework.Core.ApiClient.Provider;
using FSL.Framework.Core.ApiClient.Service;
using FSL.DatabaseQuery.Core.Service;

namespace FSL.DatabaseQuery.BlazorSrv
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(
            IServiceCollection services)
        {
            services
                .AddBlazorFslFramework(Configuration)
                .Config(opt =>
                {
                    opt.AddDefaultConfiguration();
                    opt.AddConfiguration<MyConfiguration>();
                    opt.AddFactoryService<DefaultFactoryService>();
                    opt.AddTransient<IApiClientProvider, HttpClientApiClientProvider>();
                    opt.AddSingleton<IApiClientService, DatabaseApiClientService>();
                    opt.AddSingleton<IDatabaseQueryRepository, ApiDatabaseQueryRepository>();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFslFramework();
        }
    }
}
