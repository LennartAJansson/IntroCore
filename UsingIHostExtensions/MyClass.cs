using Microsoft.Extensions.Logging;

namespace UsingIHostExtensions
{
    public class MyClass
    {
        private readonly ILogger<MyClass> logger;

        public MyClass(ILogger<MyClass> logger)
        {
            this.logger = logger;
        }
        public void DoUpgrade()
        {
            logger.LogInformation("Upgrading...");
        }
    }

}
