using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UsingIHostExtensions
{
    public static class HostExtensions
    {
        public static IHost PreHostRun(this IHost host)
        {
            var svc = host.Services.GetRequiredService<PreHostExecuter>();
            svc.Run();
            return host;
        }
    }

}
