using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UsingILogger
{
    class TestService : ITestService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<TestService> logger;

        public TestService(IConfiguration configuration, ILogger<TestService> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public Task ExecuteAsync()
        {
            logger.LogInformation("TestClass.ExecuteAsync...");
            logger.LogInformation($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            logger.LogInformation($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");

            return Task.CompletedTask;
        }
    }
}
