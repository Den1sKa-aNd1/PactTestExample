
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Interfaces;
using WebAPI.Middleware;
using WebAPI.Services;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            ConfigureAdittionalServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }
            app.UseRouting();
            app.UseHttpsRedirection();
            AddMiddleware(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("values", "api/[controller]");
            });

        }
        public virtual void ConfigureAdittionalServices(IServiceCollection services)
        {
            services.AddScoped<IValuesService, ValuesService>();
        }
        public virtual void AddMiddleware(IApplicationBuilder app) { }
    }
}
