﻿using System;

using Microsoft.Extensions.Configuration;

namespace OnlyIConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            Console.WriteLine("You now have access to a complete IConfiguration through variable configuration");
            Console.WriteLine($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
            Console.WriteLine($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
            Console.WriteLine($"  {configuration.GetSection("EnvGroup")["EnvValue"]}");
            Console.WriteLine($"  {configuration.GetSection("CmdGroup")["CmdValue"]}");
        }
    }
}
