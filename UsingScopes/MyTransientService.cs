using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingScopes
{
    internal class MyTransientService : MyBaseService
    {
        private readonly ILogger<MyTransientService> logger;

        public MyTransientService(ILogger<MyTransientService> logger)
        {
            this.logger = logger;
        }

        public override Task RunAsync()
        {
            System.Console.WriteLine($"In {nameof(MyTransientService)}, instance {Id}");
            return Task.CompletedTask;
        }
    }
}