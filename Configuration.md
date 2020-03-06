# Dependency Injection av parametrar  
  
Att man kan injecta interface till olika saker i god IOC/DI anda �r ingenting som skiljer Net Core hosting fr�n andra liknande l�sningar, det som �r intressant �r att det �r inbyggt i frameworket fr�n b�rjan och att det inte �r n�got vi m�ste s�tta upp eller konfigurera sj�lv.  
Net Core hosting inneh�ller en s� kallad ServiceCollection som har hand om registrering av allt, som standard s� har den en massa olika saker fr�n hostingen inbyggt och vi kommer att se en del av detta l�ngs v�gen i detta intro. Vi kan �ven utnyttja denna ServiceCollection f�r ytterligare registreringar av v�ra egna interface och klasser samt �ven andra tredjepartskomponenter.  
  
I detta exempel finns ett exempel p� hur man kan ta en grupp av v�rden fr�n konfigurationsfilen, omvandla denna grupp till en POCO-klass och sedan injecta denna POCO-klass som s� kallade options till v�r worker-klass. Ordningsf�ljden �r f�ljande:  
  
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
L�gg m�rke till att vi anv�nder Configure i ServiceCollection f�r att skapa en bindning mellan konfigurationsv�rdena som l�sts in till propertyn Configuration som finns i hostContext och v�r POCO-klass.  
  
Deklaration i *Worker.cs*:  
```  
private readonly TimerSettings timerSettings;
```  
  
Injection i konstruktorn f�r *Worker.cs*:  
```  
public Worker(ILogger<Worker> logger, IOptionsMonitor<TimerSettings> timerOptions)
{
    timerSettings = timerOptions.CurrentValue;
}
```  

L�gg m�rke till den injection som sker, det �r en IOptionsMonitor av v�r POCO-klass och vi hittar v�rt objekt under IOptionsMonitorn som propertyn CurrentValue.  
  
Studera exemplet h�r och k�r det f�r att se s� det fungerar.  

