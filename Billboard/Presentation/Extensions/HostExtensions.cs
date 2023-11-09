using Serilog;

namespace Presentation.Extensions;

public static class HostExtensions
{
    public static void ConfigureSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((_, configuration) =>
        {
            var path = Path.Combine(AppContext.BaseDirectory, $"logs for {DateTime.UtcNow.ToString("ddMMyyyy-hhmmss")}.log");
            configuration.WriteTo.File(path);
        });
    }
}