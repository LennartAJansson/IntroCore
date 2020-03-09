using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UsingIHostExtensions
{
    public static class HostExtensions
    {
        public static IHost DoSomethingBeforeRun(this IHost host)
        {
            var svc = host.Services.GetRequiredService<MyClass>();
            svc.DoUpgrade();
            return host;
        }
    }

}
