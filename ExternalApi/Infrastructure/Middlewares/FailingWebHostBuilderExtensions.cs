using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ExternalApi.Infrastructure.Middlewares
{
    public static class FailingWebHostBuilderExtensions
    {
        public static IHostBuilder UseFailing(this IHostBuilder builder, Action<FailingOptions> options)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter>(new FailingStartupFilter(options));
            });
            return builder;
        }
    }
}
