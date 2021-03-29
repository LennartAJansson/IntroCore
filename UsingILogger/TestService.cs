using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingILogger
{
    internal class TestService : ITestService
    {
        private readonly ILogger<TestService> logger;
        private readonly IConfiguration configuration;

        public TestService(ILogger<TestService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public Task ExecuteAsync()
        {
            logger.LogTrace("TestClass.ExecuteAsync...");
            logger.LogTrace($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            logger.LogTrace($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
            logger.LogTrace($"  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            logger.LogTrace($"  {configuration.GetSection("CmdGroup")["CmdValue"]}");

            return Task.CompletedTask;
        }
    }
}