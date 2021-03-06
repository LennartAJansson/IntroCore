## Using IConfiguration

The configuration in NET Core Extensions is handled by several Configuration Providers where each provider's role is to make the type of configuration data available in a common, generic way. There are providers not only for normal json or xml files but also for Environment variables, Command line, Azure Keyvault, User Secrets as well as SQL based configuration and it is quite easy to create your own providers to cover any specific needs.

In this document I will explain how they are created and used from a traditional Console application, without any extra demands.

To enable basic configuration handling it only requires adding one reference.

UsingIConfiguration.csproj:

```xml
<PackageReference Include="Microsoft.Extensions.Configuration" Version="3.1.2" />
```

But to add the different Configuration Providers you also need to add references for each one of them. This example will use a basic configuration with appsettings.json, Environment variables and Command line. You will also see how the different providers compare to each others. Here's the references for the providers that you will use.

UsingIConfiguration.csproj:

```xml
<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="3.1.2" /><PackageReference Include="Microsoft.Extensions.Configuration.EnvironmentVariables" Version="3.1.2" />
<PackageReference Include="Microsoft.Extensions.Configuration.CommandLine" Version="3.1.2" />
```

Next you create the different configurations that will be read from the application, for the appsettings.json add a new json file, make sure it's marked with Copy to output directory - Copy always.

appsettings.json:

```
{
  "GlobalGroup": {
    "GlobalValue": "This is a global value"
  }
}
```

Next, open up the properties for the project and select the tab named Debug, in the Application arguments you add following string:

`CmdGroup:CmdValue="This is a value from commandline"`

And in the grid for Environmental variables you add:

| Name               | Value                            |
| ------------------ | -------------------------------- |
| EnvGroup__EnvValue | This is a value from environment |

Notice the difference in the naming convention for each type of value! 

CmdGroup:CmdValue is an alternative way to write a json statement with a group named CmdGroup and an element name of CmdValue having the string value "This is a value from commandline"

In Environment the colon (:) character is reserved by the operating system and not allowed to use. The configuration provider for Environment will instead replace a double underscore (__) with a colon when read, so it will be translated into the same format, EnvGroup:EnvValue="This is a value from environment".

So, finally the code that makes use of all this. 

Program.cs:

```
static void Main(string[] args)
{
    IConfiguration configuration = new ConfigurationBuilder()
    	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    	.AddEnvironmentVariables()
    	.AddCommandLine(args)
    	.Build();

    Console.WriteLine("You now have access to a complete IConfiguration");
    Console.WriteLine($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
    Console.WriteLine($"  {configuration.GetSection("EnvGroup")["EnvValue"]}");
    Console.WriteLine($"  {configuration.GetSection("CmdGroup")["CmdValue"]}");
}
```

Start by creating a ConfigurationBuilder, to the builder you add the json file with the method AddJson, then add the Environment variables with the method AddEnvironmentVariables and finally add the commandline with the method AddCommandLine.

AddJson takes a couple of parameters, the filename, if it should be optional (throw exception or not if it doesn't exist) and if it should be reloaded automatically if the content changes during application execution. This last parameter will be further explained in [Using dynamic configuration injection](UsingDynamicConfigInjection.md).

AddEnvironmentVariables doesn't take any parameters.

AddCommandline takes one parameter, the string array args sent to the application when started.

Once you call the Build method of the ConfigurationBuilder it will generate an IConfiguration containing these three configurations.

Afterwards you can use the common way of accessing these configuration groups and values by the method IConfiguration.GetSection(groupName)[valueName].