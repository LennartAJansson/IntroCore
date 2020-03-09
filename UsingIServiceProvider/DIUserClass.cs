using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace UsingIServiceProvider
{
    class DIUserClass
    {
        private readonly ITestService testService;
        private readonly IConfiguration configuration;

        public DIUserClass(ITestService testService, IConfiguration configuration)
        {
            this.testService = testService;
            this.configuration = configuration;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("DIUserClass.RunAsync...");
            Console.WriteLine($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            Console.WriteLine($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
            Console.WriteLine($"  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            Console.WriteLine($"  {configuration.GetSection("CmdGroup")["CmdValue"]}");

            await testService.ExecuteAsync();
        }
    }
}
