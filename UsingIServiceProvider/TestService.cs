using Microsoft.Extensions.Configuration;

using System;
using System.Threading.Tasks;

namespace UsingIServiceProvider
{
    internal class TestService : ITestService
    {
        private readonly IConfiguration configuration;

        public TestService(IConfiguration configuration) => this.configuration = configuration;

        public Task ExecuteAsync()
        {
            Console.WriteLine("TestClass.ExecuteAsync... You now have access to a complete IConfiguration through variable configuration and dependency injection");

            Console.WriteLine($"GetSection(\"GlobalGroup\")[\"GlobalValue\"]:  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            Console.WriteLine($"[\"GlobalGroup:GlobalValue\"]:  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            Console.WriteLine();

            Console.WriteLine($"GetSection(\"DevelopmentGroup\")[\"DevelopmentValue\"]:  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
            Console.WriteLine($"[\"DevelopmentGroup:DevelopmentValue\"]:  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
            Console.WriteLine();

            Console.WriteLine($"GetSection(\"EnvGroup\")[\"EnvValue\"];  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            Console.WriteLine($"[\"EnvGroup:EnvValue\"];  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            Console.WriteLine();

            Console.WriteLine($"GetSection(\"CmdGroup\")[\"CmdValue\"];  {configuration.GetSection("CmdGroup")["CmdValue"]}");
            Console.WriteLine($"[\"CmdGroup:CmdValue\"];  {configuration.GetSection("CmdGroup")["CmdValue"]}");
            Console.WriteLine();

            return Task.CompletedTask;
        }
    }
}