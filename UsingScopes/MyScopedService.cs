using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingScopes
{
    internal class MyScopedService : MyBaseService
    {
        private readonly ILogger<MyScopedService> logger;

        public MyScopedService(ILogger<MyScopedService> logger)
            : base()
        {
            this.logger = logger;
        }

        public override Task RunAsync()
        {
            System.Console.WriteLine($"In {nameof(MyScopedService)}, instance {Id}");
            return Task.CompletedTask;
        }
    }
}