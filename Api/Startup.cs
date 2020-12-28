using Api.Extensions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace Api
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
            services.Configure<Settings>(Configuration.GetSection(nameof(Settings)));
            services.AddSingleton(sp => sp.GetService<IOptions<Settings>>().Value);

            services.AddControllers();
            services.AddHealthChecks()
                .AddCheck<ExternalHealthCheck>("ExternalAPI");

            services
                .AddHttpClient<WeatherService>("externalApi", client =>
                    {
                        System.Console.WriteLine($"{Configuration["Settings:BaseExternalUrl"]}:{Configuration["Settings:Port"]}");
                        client.BaseAddress = new Uri($"{Configuration["Settings:BaseExternalUrl"]}:{Configuration["Settings:Port"]}");
                    })
                .AddRetryPolicy()
                .AddCircuitBreakerPolicy();

            services.AddScoped<WeatherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/status");
            });
        }
    }
}
