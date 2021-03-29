using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingILogger
{
    internal class DIUserClass
    {
        private readonly ILogger<DIUserClass> logger;
        private readonly ITestService testService;
        private readonly IConfiguration configuration;

        public DIUserClass(ILogger<DIUserClass> logger, ITestService testService, IConfiguration configuration)
        {
            this.logger = logger;
            this.testService = testService;
            this.configuration = configuration;
        }

        public async Task ExecuteAsync()
        {
            logger.LogTrace("DIUserClass.RunAsync...");
            logger.LogDebug($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            logger.LogInformation($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
            logger.LogWarning($"  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            logger.LogError($"  {configuration.GetSection("CmdGroup")["CmdValue"]}");
            logger.LogCritical($"  {configuration.GetSection("CmdGroup")["CmdValue"]}");

            await testService.ExecuteAsync();
        }
    }
}