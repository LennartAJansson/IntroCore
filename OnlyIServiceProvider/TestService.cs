using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace OnlyIServiceProvider
{
    class TestService : ITestService
    {
        private readonly IConfiguration configuration;

        public TestService(IConfiguration configuration) => this.configuration = configuration;

        public Task ExecuteAsync()
        {
            Console.WriteLine("TestClass.ExecuteAsync...");
            Console.WriteLine($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            Console.WriteLine($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
            Console.WriteLine($"  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            Console.WriteLine($"  {configuration.GetSection("CmdGroup")["CmdValue"]}");

            return Task.CompletedTask;
        }
    }

}
