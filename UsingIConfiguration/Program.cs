using Microsoft.Extensions.Configuration;

using System;

namespace UsingIConfiguration
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string envKind = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) //GlobalGroup:GlobalValue
                .AddJsonFile($"appsettings.{envKind}.json", optional: true, reloadOnChange: true) //DevelopmentGroup:DevelopmentValue
                .AddEnvironmentVariables() //EnvGroup__EnvValue
                .AddCommandLine(args) //CmdGroup:CmdValue
                .Build();

            Console.WriteLine("You now have access to a complete IConfiguration through variable configuration");

            Console.WriteLine($"GetSection(\"GlobalGroup\")[\"GlobalValue\"]:  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            Console.WriteLine($"[\"GlobalGroup:GlobalValue\"]:  {configuration["GlobalGroup:GlobalValue"]}");
            Console.WriteLine();

            Console.WriteLine($"GetSection(\"DevelopmentGroup\")[\"DevelopmentValue\"]:  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
            Console.WriteLine($"[\"DevelopmentGroup:DevelopmentValue\"]:  {configuration["DevelopmentGroup:DevelopmentValue"]}");
            Console.WriteLine();

            Console.WriteLine($"GetSection(\"EnvGroup\")[\"EnvValue\"];  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            Console.WriteLine($"[\"EnvGroup:EnvValue\"];  {configuration["EnvGroup:EnvValue"]}");
            Console.WriteLine();

            Console.WriteLine($"GetSection(\"CmdGroup\")[\"CmdValue\"];  {configuration.GetSection("CmdGroup")["CmdValue"]}");
            Console.WriteLine($"[\"CmdGroup:CmdValue\"];  {configuration["CmdGroup:CmdValue"]}");
            Console.WriteLine();
        }
    }
}