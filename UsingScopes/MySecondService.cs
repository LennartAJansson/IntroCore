using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingScopes
{
    internal class MySecondService : MyBaseService
    {
        private readonly ILogger<MySecondService> logger;
        private readonly MyBaseService myFourthService;

        //We inject our second InjectedService here
        public MySecondService(ILogger<MySecondService> logger, MyBaseService myFourthService)
        {
            this.logger = logger;
            this.myFourthService = myFourthService;
        }

        public Task RunAsync()
        {
            logger.LogInformation($"In MySecondService, Fourth service id: {myFourthService.Id}");
            return Task.CompletedTask;
        }
    }
}