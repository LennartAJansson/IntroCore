# Dependency Injection av parametrar  
  
Att man kan injecta interface till olika saker i god IOC/DI anda är ingenting som skiljer Net Core hosting från andra liknande lösningar, det som är intressant är att det är inbyggt i frameworket från början och att det inte är något vi måste sätta upp eller konfigurera själv.  
Net Core hosting innehåller en så kallad ServiceCollection som har hand om registrering av allt, som standard så har den en massa olika saker från hostingen inbyggt och vi kommer att se en del av detta längs vägen i detta intro. Vi kan även utnyttja denna ServiceCollection för ytterligare registreringar av våra egna interface och klasser samt även andra tredjepartskomponenter.  
  
I detta exempel finns ett exempel på hur man kan ta en grupp av värden från konfigurationsfilen, omvandla denna grupp till en POCO-klass och sedan injecta denna POCO-klass som så kallade options till vår worker-klass. Ordningsföljden är följande:  
  
I konfigurationsfilen *appsettings.json*:  
```  
"TimerSettings": {
  "TimerSeconds": 5
}
```  
  
POCO-klass *TimerSettings.cs*:  
```  
internal class TimerSettings
{
    public int TimerSeconds { get; set; }
}
```  
  
Registrering i ServiceCollection i *Program.cs*:  
```  
.ConfigureServices((hostContext, services) =>
{
    services.AddOptions();
    services.Configure<TimerSettings>(hostContext.Configuration.GetSection("TimerSettings"));
    services.AddHostedService<Worker>();
});
```  
Lägg märke till att vi använder Configure i ServiceCollection för att skapa en bindning mellan konfigurationsvärdena som lästs in till propertyn Configuration som finns i hostContext och vår POCO-klass.  
  
Deklaration i *Worker.cs*:  
```  
private readonly TimerSettings timerSettings;
```  
  
Injection i konstruktorn för *Worker.cs*:  
```  
public Worker(ILogger<Worker> logger, IOptionsMonitor<TimerSettings> timerOptions)
{
    timerSettings = timerOptions.CurrentValue;
}
```  

Lägg märke till den injection som sker, det är en IOptionsMonitor av vår POCO-klass och vi hittar vårt objekt under IOptionsMonitorn som propertyn CurrentValue.  
  
Studera exemplet här och kör det för att se så det fungerar.  

