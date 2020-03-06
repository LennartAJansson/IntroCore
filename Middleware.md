# Middleware  
Domain Driven Design anv�nder sig av en metod d�r man delar av komponenter som tillhandah�ller tj�nster �t mjukvaran. Man kallar detta f�r services och de �r indelade i tre huvudkategorier:  
* Infrastructure Services, allt som st�djer det som �r externt (db, disk, kommunikation mm)  
* Domain Services, allt som st�djer internt inom v�r dom�n  
* Application Services, allt som st�djer applikationen  

Denna del av introt kommer att visa hur man g�r ett par Infrastructure services lite mer l�ttanv�nda och �teranv�ndbara genom att anv�nda ett begrepp som i NET Core beskrivs som Middleware.  

Att g�ra om sin kod till att f�lja NET Core's rekommendationer runt Middleware �r egentligen ganska enkelt i grunden. Det handlar om att g�mma undan s� mycket som m�jligt av konkretionen utanf�r f�rbrukaren och d�rigenom uppn� l�sa kopplingar, lite grann som ett factory. Dom�nen/Applikationen beh�ver inte veta hur man faktiskt kommunicerar via UDP Broadcast, de beh�ver bara ha ett interface som ger dem en kontaktyta mot denna funktionalitet.  

Genom att dela in sin kod som i detta exempel s� uppn�r man att applikationerna endast har direkta kopplingar till abstraktionen och till extension/factory metoderna. I koden blir det dessutom mycket snyggare och tydligare/l�ttl�st och man kan implementera funktionaliteten p� ett v�ldigt enkelt s�tt!    
<pre><code>  
public static IHostBuilder CreateHostBuilder(string[] args) =>  
    Host.CreateDefaultBuilder(args)  
        <b>.UseServiceTimer("TimerSettings")</b>  
        <b>.UseUdpSpeaker("SpeakerConfig")</b>  
        .ConfigureServices((hostContext, services) =>  
        {  
            services.AddHostedService<Worker>();  
        });  
</code></pre> 
I koden h�r ovan ser vi tv� olika Middleware extensions, **UseServiceTimer** som �r en intern extension f�r att f�renkla kodf�rst�elsen och **UseUdpSpeaker** som �r en extern extension f�r att "jacka in" en process p� ett enkelt s�tt. B�da tar en str�ngparameter som �r namnet p� den grupp i konfigurationsfilen som inneh�ller dess konfiguration.  
  
Som exempel p� hur man skriver sina servicar som middleware har jag anv�nt ett grundl�ggande kommunikationsexempel. Att skicka meddelanden via UDP Broadcast.  

Exemplet �r uppdelat i ett antal assemblies och applikationer:  
* Udp.Abstract, all abstraktion f�r f�r Udp.Core  
* Udp.Core, all konkretion f�r processerna  
* Udp.Extensions, extensionmetoder f�r att implementera UDP i applikationerna  
* Udp.Listener, en BackgroundService som ligger och lyssnar efter UDP Broadcast meddelanden och svarar tillbaka vad den har tagit emot  
* Udp.TimedSender, en BackgroundService som ligger och skickar UDP Broadcast meddelanden  
* Udp.WebSender, en webapplikation som kan skicka UDP Broadcast p� server side  

L�gg m�rke till i Udp.Extensions att det finns en metod som heter **UseUdpSpeaker** och en som heter **AddUdpSpeaker**, skillnaden mellan dessa ligger i att de �r extensions f�r **IHostBuilder** respektive **IServiceCollection**. Inneh�llet d�remot �r i princip detsamma. Detta �r gjort f�r att vi ska kunna se att denna typ av extensions kan anv�ndas p� b�da niv�er, vi skiljer p� metoderna genom de rekommenderade **Add** och **Use** prefixen.  

Middleware �r s� otroligt mycket mer �n det lilla vi n�mnt h�r, se vidare:  
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/index?view=aspnetcore-3.0

