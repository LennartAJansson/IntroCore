using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UsingILogger
{
    class DIUserClass
    {
        private readonly ITestService testService;
        private readonly IConfiguration configuration;
        private readonly ILogger<DIUserClass> logger;

        public DIUserClass(ITestService testService, IConfiguration configuration, ILogger<DIUserClass> logger)
        {
            this.testService = testService;
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task RunAsync()
        {
            logger.LogInformation("DIUserClass.RunAsync...");
            logger.LogInformation($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            logger.LogInformation($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");

            await testService.ExecuteAsync();
        }
    }
}
