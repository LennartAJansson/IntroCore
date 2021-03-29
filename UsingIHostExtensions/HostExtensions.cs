using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UsingIHostExtensions
{
    //Mention what happens when you add this class to the namespace Microsoft.Extensions.DependencyInjection
    public static class HostExtensions
    {
        public static IHost PreHostRun(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            //Could use shortened version:
            //scope.ServiceProvider.GetRequiredService<PreHostExecuter>().Run();

            var svc = scope.ServiceProvider
                .GetRequiredService<PreHostExecuter>();

            svc.Run();

            return host;
        }
    }
}