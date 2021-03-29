using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingScopes
{
    internal class MyTransientService
    {
        private readonly ILogger<MyTransientService> logger;

        //We inject our second InjectedService here
        public MyTransientService(ILogger<MyTransientService> logger)
        {
            this.logger = logger;
        }

        public Task RunAsync()
        {
            return Task.CompletedTask;
        }
    }
}