using System;

using Microsoft.Extensions.Configuration;

namespace UsingIConfiguration
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables() //EnvGroup__EnvValue
                .AddCommandLine(args)
                .Build();

            Console.WriteLine("You now have access to a complete IConfiguration through variable configuration");
            Console.WriteLine($"GetSection(\"GlobalGroup\")[\"GlobalValue\"]:  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            Console.WriteLine($"[\"GlobalGroup:GlobalValue\"]:  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            Console.WriteLine($"GetSection(\"EnvGroup\")[\"EnvValue\"];  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            Console.WriteLine($"[\"EnvGroup:EnvValue\"];  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            Console.WriteLine($"GetSection(\"CmdGroup\")[\"CmdValue\"];  {configuration.GetSection("CmdGroup")["CmdValue"]}");
            Console.WriteLine($"[\"CmdGroup:CmdValue\"];  {configuration.GetSection("CmdGroup")["CmdValue"]}");
        }
    }
}