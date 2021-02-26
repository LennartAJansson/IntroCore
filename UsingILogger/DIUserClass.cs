using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingILogger
{
    internal class DIUserClass
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
            logger.LogTrace("DIUserClass.RunAsync...");
            logger.LogTrace($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            logger.LogTrace($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");

            await testService.ExecuteAsync();
        }
    }
}