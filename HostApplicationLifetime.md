# HostApplicationLifetime
  
Som ytterligare exempel p� vad som erbjuds out-of-the-box kan vi titta p� vad som f�ljer med om vi injectar exempelvis interfacet IHostApplicationLifetime:  
  
Vi b�rjar med att deklarera en variabel f�r det i *Worker.cs*:  
```  
private readonly IHostApplicationLifetime appLifetime;
```  
  
Sen injectar vi ett v�rde f�r denna variabel i konstruktorn:  
```  
public Worker(IHostApplicationLifetime appLifetime)
{
    this.appLifetime = appLifetime;
}
```  
  
I StartAsync kan vi d� initiera f�ljande:  
```  
public override async Task StartAsync(CancellationToken cancellationToken)
{
    appLifetime.ApplicationStarted.Register(OnStarted);
    appLifetime.ApplicationStopping.Register(OnStopping);
    appLifetime.ApplicationStopped.Register(OnStopped);

    await base.StartAsync(cancellationToken);
}
```    
  
Resultatet blir att vi har f�ljande events att anv�nda oss av:  
  
```  
private void OnStarted() => logger.LogInformation("OnStarted");

private void OnStopping() => logger.LogInformation("OnStopping");

private void OnStopped() => logger.LogInformation("OnStopped");

```  
  
Det finns m�nga fler interface att injecta p� liknande s�tt, det b�sta �r att utforska f�ljande l�nkar d�r det finns mycket information runt det som har beskrivits s� h�r l�ngt i detta intro:  
[Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0)  
[Hosted Services](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.0&tabs=visual-studio)
  
