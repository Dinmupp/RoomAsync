using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;

namespace Prometheus.Middleware
{
    public static class PrometheusExporter
    {
        public static IServiceCollection AddPrometheusExporter(this IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddPrometheusExporter();
            services.AddOpenTelemetry()
                .WithMetrics(builder =>
                {
                    builder.AddPrometheusExporter();
                });
            return services;
        }
    }
}
