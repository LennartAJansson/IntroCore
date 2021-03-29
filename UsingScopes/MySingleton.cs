using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingScopes
{
    internal class MySingleton : MyBaseService
    {
        private readonly ILogger<MySingleton> logger;

        public MySingleton(ILogger<MySingleton> logger)
        {
            this.logger = logger;
        }

        public Task RunAsync()
        {
            return Task.CompletedTask;
        }
    }
}