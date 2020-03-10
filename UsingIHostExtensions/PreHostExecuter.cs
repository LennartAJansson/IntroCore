using Microsoft.Extensions.Logging;

namespace UsingIHostExtensions
{
    public class PreHostExecuter
    {
        private readonly ILogger<PreHostExecuter> logger;

        public PreHostExecuter(ILogger<PreHostExecuter> logger)
        {
            this.logger = logger;
        }
        public void Run()
        {
            logger.LogInformation("Upgrading...");
        }
    }

}
