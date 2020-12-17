using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingScopes
{
    internal class MyMainService
    {
        private readonly ILogger<MyMainService> logger;
        private readonly InjectedService injectedService;
        private readonly MySecondService mySecondService;

        //We inject our first InjectedService here
        public MyMainService(ILogger<MyMainService> logger, InjectedService injectedService, MySecondService mySecondService)
        {
            this.logger = logger;
            this.injectedService = injectedService;
            this.mySecondService = mySecondService;
        }

        public async Task RunAsync()
        {
            logger.LogInformation($"In MyMainService, Injected service id: {injectedService.Id}");
            await mySecondService.RunAsync();
        }
    }
}