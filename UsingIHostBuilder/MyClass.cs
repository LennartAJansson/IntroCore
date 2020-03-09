using Microsoft.Extensions.Logging;

namespace UsingIHostBuilder
{
    public class MyClass
    {
        private readonly ILogger<MyClass> logger;

        public MyClass(ILogger<MyClass> logger)
        {
            this.logger = logger;
        }
    }
}
