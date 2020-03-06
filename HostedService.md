# Hosted Services

En av de nyare tillskotten i Net Core �r interfacet IHostedService, med hj�lp av dettainterface kan man p� ett enkelt s�tt f� en service upp att snurra helt automatiskt. Det enda som beh�vs �r f�ljande:  

* Skapa en Net Core Console Application  
* L�gg till en referens till Microsoft.Extensions.Hosting fr�n NuGet  
* Skapa en klass som implementerar interfacet IHostedService enligt nedan  
* L�gg till nedanst�ende kod i Program.cs  

Klass med implementation av IHostedService:  
```
internal class Worker2 : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken) => Console.WriteLine("Starting service");
    public Task StopAsync(CancellationToken cancellationToken) => Console.WriteLine("Stopping service");
}
```  
  
Inneh�ll i Program:  
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
  
Grattis, du har nu skrivit en bakgrundsservice i en selfhosted application, full kapabel att k�ra i en container!  

Om du nu tittar i exempelkoden s� ser ni ett denna Worker.cs inneh�ller en timer och lite mer kod, detta �r bara f�r att visa att allt faktiskt fungerar.  

Vidare om du k�r programmet ska du se att det loggas lite extra information i f�nstret, det �r Host-frameworket som inneh�ller en massa bakom fasaden, bl a en ILogger som hanterar loggning automatiskt, men �ven en sk SIGC handling vilket inneb�r att programmet reagerar p� bl a Ctrl-C. K�r programmet, efter 10-15 sekunder tryck Ctrl-C och ta sen och studera vad som skrivits p� sk�rmen!  

