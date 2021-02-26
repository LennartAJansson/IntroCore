using Microsoft.Extensions.Logging;

namespace UsingIHostBuilder
{
    public class MyClass
    {
        private readonly ILogger<MyClass> logger;

        public MyClass(ILogger<MyClass> logger)
        {
            this.logger = logger;
            this.logger.LogInformation("Creating MyClass");
        }

        public void Execute()
        {
            logger.LogInformation("In MyClass.Execute...");
        }
    }
}