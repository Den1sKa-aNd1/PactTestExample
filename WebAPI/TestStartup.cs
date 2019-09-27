using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Fakes;
using WebAPI.Interfaces;
using WebAPI.Middleware;

namespace WebAPI
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration): base(configuration)
        {
        }

        public override void AddMiddleware(IApplicationBuilder app)
        {
            app.UseMiddleware<ProviderStateMiddleware>();
        }

        public override void ConfigureAdittionalServices(IServiceCollection services)
        {
            services.AddScoped<IValuesService, FakeValueService>();
        }
    }
}
