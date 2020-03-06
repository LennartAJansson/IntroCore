# Hosted Services

En av de nyare tillskotten i Net Core är interfacet IHostedService, med hjälp av dettainterface kan man på ett enkelt sätt få en service upp att snurra helt automatiskt. Det enda som behövs är följande:  

* Skapa en Net Core Console Application  
* Lägg till en referens till Microsoft.Extensions.Hosting från NuGet  
* Skapa en klass som implementerar interfacet IHostedService enligt nedan  
* Lägg till nedanstående kod i Program.cs  

Klass med implementation av IHostedService:  
```
internal class Worker2 : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken) => Console.WriteLine("Starting service");
    public Task StopAsync(CancellationToken cancellationToken) => Console.WriteLine("Stopping service");
}
```  
  
Innehåll i Program:  
```
class Program
{
    private static void Main(string[] args) =>
        CreateHostBuilder(args).Build().Run();

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
                services.AddHostedService<Worker>());
}
```  
  
Grattis, du har nu skrivit en bakgrundsservice i en selfhosted application, full kapabel att köra i en container!  

Om du nu tittar i exempelkoden så ser ni ett denna Worker.cs innehåller en timer och lite mer kod, detta är bara för att visa att allt faktiskt fungerar.  

Vidare om du kör programmet ska du se att det loggas lite extra information i fönstret, det är Host-frameworket som innehåller en massa bakom fasaden, bl a en ILogger som hanterar loggning automatiskt, men även en sk SIGC handling vilket innebär att programmet reagerar på bl a Ctrl-C. Kör programmet, efter 10-15 sekunder tryck Ctrl-C och ta sen och studera vad som skrivits på skärmen!  

