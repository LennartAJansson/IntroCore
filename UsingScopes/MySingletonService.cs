using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingScopes
{
    internal class MySingletonService : MyBaseService
    {
        private readonly ILogger<MySingletonService> logger;

        public MySingletonService(ILogger<MySingletonService> logger)
            : base()
        {
            this.logger = logger;
        }

        public override Task RunAsync()
        {
            System.Console.WriteLine($"In {nameof(MySingletonService)}, instance {Id}");
            return Task.CompletedTask;
        }
    }
}