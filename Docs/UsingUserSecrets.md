

## Using User Secrets

User Secrets lets you store sensitive configuration information in a protected location outside of your version control and under your user profile. The physical path for the configuration file is "%appdata%\Microsoft\UserSecrets\" plus the value of the xml element UserSecretsId in your projectfile. Inside that folder there will be a file named secrets.json and it follows standard json configuration format.

To enable the use of UserSecrets you start off by adding a reference to the extension Nuget for it.

UsingUserSecrets.csproj:

```xml
<PackageReference Include="Microsoft.Extensions.Configuration.UserSecrets" Version="3.1.2" />
```
Then you can go ahead and configure your user secrets in the secrets.json, you could open the file from the file system but the best way to do it is to rightclick your project insde Visual Studio and choose "Manage User Secrets" in the menu. Add following content to the file.

Secrets.json:

```json
{
	"SampleUserSecret": {
		"ConnectionString": "This is my connectionstring"
	}
}
```

Once you have finished previous step you can continue by adding a class that will be the bearer of your UserSecret data, this is an plain old CLR object class, a POCO class.

SampleUserSecret.cs:


```c#
internal class SampleUserSecret
{
	public string ConnectionString { get; set; }
}
```

Then we prepare the Worker class to receive an IOptions of SampleUserSecret by injection, to get more information about IOptions please refer to the documents [UsingConfigInjection.md](UsingConfigInjection.md) and [UsingDynamicConfigInjection.md](UsingDynamicConfigInjection.md):

Worker.cs:

```c#
internal class Worker : BackgroundService
{
	private readonly ILogger<Worker> logger;
	private readonly string connectionString;

	public Worker(ILogger<Worker> logger, IOptions<SampleUserSecret> options)
	{
		this.logger = logger;
		connectionString = options.Value.ConnectionString;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			logger.LogInformation($"Worker running at: {DateTimeOffset.Now}," + 
                                  " connectionString is: {connectionString}");
			await Task.Delay(1000, stoppingToken);
		}
	}
}
```

And finally you put all pieces together in the CreateHostBuilder method in the Program class.

Program.cs:

```
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddOptions();
            services.Configure<SampleUserSecret>(options =>
                hostContext.Configuration.GetSection("SampleUserSecret").Bind(options));
            services.AddHostedService<Worker>();
        });
```

Earlier versions of NET Core Extensions didn't include UserSecrets by itself, so the CreateHostBuilder method had to take care of it for you by calling ConfigureAppConfiguration:

Program.cs (old way):

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        	.ConfigureAppConfiguration(config =>
        	    config.AddUserSecrets<Program>(optional: true))
            .ConfigureServices((hostContext, services) =>
            {
                services.AddOptions();
                services.Configure<SampleUserSecret>(options =>
                    hostContext.Configuration.GetSection("SampleUserSecret").Bind(options));
                services.AddHostedService<Worker>();
            });
But, that is not needed anymore.

Remember that this way of storing sensitive data is pretty much connected to your Visual Studio environment, but you do this outside of Visual Studio with the DotNet CLI (if you're using Visual Studio Code for example). Just open a command prompt in your projectfile folder and execute following commands:

`dotnet user-secrets init`

This will create a secrets folder and the secrets.json inside, it will also add the foldername for your secrets in the xml element UserSecretsId inside your project file. 

`dotnet user-secrets set "SampleUserSecret:ConnectionString" "This is my connectionstring"`

This command will create the same secret as the one described in the beginning of this document.

`dotnet user-secrets --help`

If you would like to see other options that you have for managing user secrets from DotNet CLI.