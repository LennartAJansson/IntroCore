using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace UsingILogger
{
    internal class TestService : ITestService
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
            logger.LogTrace("TestClass.ExecuteAsync...");
            logger.LogTrace($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            logger.LogTrace($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");

            return Task.CompletedTask;
        }
    }
}