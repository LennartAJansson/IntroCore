# HostApplicationLifetime
  
Som ytterligare exempel på vad som erbjuds out-of-the-box kan vi titta på vad som följer med om vi injectar exempelvis interfacet IHostApplicationLifetime:  
  
Vi börjar med att deklarera en variabel för det i *Worker.cs*:  
```  
private readonly IHostApplicationLifetime appLifetime;
```  
  
Sen injectar vi ett värde för denna variabel i konstruktorn:  
```  
public Worker(IHostApplicationLifetime appLifetime)
{
    this.appLifetime = appLifetime;
}
```  
  
I StartAsync kan vi då initiera följande:  
```  
public override async Task StartAsync(CancellationToken cancellationToken)
{
    appLifetime.ApplicationStarted.Register(OnStarted);
    appLifetime.ApplicationStopping.Register(OnStopping);
    appLifetime.ApplicationStopped.Register(OnStopped);

    await base.StartAsync(cancellationToken);
}
```    
  
Resultatet blir att vi har följande events att använda oss av:  
  
```  
private void OnStarted() => logger.LogInformation("OnStarted");

private void OnStopping() => logger.LogInformation("OnStopping");

private void OnStopped() => logger.LogInformation("OnStopped");

```  
  
Det finns många fler interface att injecta på liknande sätt, det bästa är att utforska följande länkar där det finns mycket information runt det som har beskrivits så här långt i detta intro:  
[Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0)  
[Hosted Services](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.0&tabs=visual-studio)
  
