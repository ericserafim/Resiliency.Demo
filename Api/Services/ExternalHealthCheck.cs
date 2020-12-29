using Api.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ExternalHealthCheck : IHealthCheck
    {
        private readonly Settings _settings;

        public ExternalHealthCheck(Settings settings)
        {
            _settings = settings;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await Task.Run(() => 
                {
                    System.Console.WriteLine($"{_settings.BaseExternalUrl.Replace("http://", string.Empty).Replace("https://", string.Empty)}:{_settings.Port}");                

                    using var tcpClient = new TcpClient(
                        _settings.BaseExternalUrl.Replace("https://", string.Empty), 
                        _settings.Port);
                });
                
                return HealthCheckResult.Healthy();
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy("ExternalAPI", e);
            }
        }
    }
}
