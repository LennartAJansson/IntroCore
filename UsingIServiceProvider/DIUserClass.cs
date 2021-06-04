using Microsoft.Extensions.Configuration;

using System;
using System.Threading.Tasks;

namespace UsingIServiceProvider
{
    internal class DIUserClass
    {
        private readonly ITestService testService;
        private readonly IConfiguration configuration;

        public DIUserClass(ITestService testService, IConfiguration configuration)
        {
            this.testService = testService;
            this.configuration = configuration;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("DIUserClass.ExecuteAsync... You now have access to a complete IConfiguration through variable configuration and dependency injection");

            Console.WriteLine($"GetSection(\"JsonGroup\")[\"JsonValue\"]:  {configuration.GetSection("JsonGroup")["JsonValue"]}");
            Console.WriteLine($"[\"JsonGroup:JsonValue\"]:  {configuration.GetSection("JsonGroup")["JsonValue"]}");
            Console.WriteLine();

            Console.WriteLine($"GetSection(\"DevelopmentGroup\")[\"DevelopmentValue\"]:  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
            Console.WriteLine($"[\"DevelopmentGroup:DevelopmentValue\"]:  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
            Console.WriteLine();

            Console.WriteLine($"GetSection(\"YamlGroup\")[\"YamlValue\"]:  {configuration.GetSection("YamlGroup")["YamlValue"]}");
            Console.WriteLine($"[\"YamlGroup:YamlValue\"]:  {configuration["YamlGroup:YamlValue"]}");
            Console.WriteLine();

            Console.WriteLine($"GetSection(\"EnvGroup\")[\"EnvValue\"];  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            Console.WriteLine($"[\"EnvGroup:EnvValue\"];  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            Console.WriteLine();

            Console.WriteLine($"GetSection(\"CmdGroup\")[\"CmdValue\"];  {configuration.GetSection("CmdGroup")["CmdValue"]}");
            Console.WriteLine($"[\"CmdGroup:CmdValue\"];  {configuration.GetSection("CmdGroup")["CmdValue"]}");
            Console.WriteLine();

            await testService.ExecuteAsync();
        }
    }
}