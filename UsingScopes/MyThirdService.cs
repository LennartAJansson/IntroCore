using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingScopes
{
    internal class MyThirdService
    {
        private readonly ILogger<MyThirdService> logger;
        private readonly InjectedService injectedService;

        //We inject our second InjectedService here
        public MyThirdService(ILogger<MyThirdService> logger, InjectedService injectedService)
        {
            this.logger = logger;
            this.injectedService = injectedService;
        }

        public Task RunAsync()
        {
            logger.LogInformation($"In MyThirdService, Injected service id: {injectedService.Id}");
            return Task.CompletedTask;
        }
    }
}