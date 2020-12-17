using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingScopes
{
    internal class MySecondService
    {
        private readonly ILogger<MySecondService> logger;
        private readonly InjectedService injectedService;

        //We inject our second InjectedService here
        public MySecondService(ILogger<MySecondService> logger, InjectedService injectedService)
        {
            this.logger = logger;
            this.injectedService = injectedService;
        }

        public Task RunAsync()
        {
            logger.LogInformation($"In MySecondService, Injected service id: {injectedService.Id}");
            return Task.CompletedTask;
        }
    }
}