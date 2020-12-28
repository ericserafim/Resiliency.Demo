using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api.Extensions
{
    public static class PolicyExtensions
    {
        public static IHttpClientBuilder AddRetryPolicy(this IHttpClientBuilder builder)
        {
            builder.AddPolicyHandler(GetRetryPolicy());
            return builder;
        }

        public static IHttpClientBuilder AddCircuitBreakerPolicy(this IHttpClientBuilder builder)
        {
            builder.AddPolicyHandler(GetCircuitBreakerPolicy());
            return builder;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(10));
        }
    }
}
