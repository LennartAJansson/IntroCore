# Middleware  
Domain Driven Design använder sig av en metod där man delar av komponenter som tillhandahåller tjänster åt mjukvaran. Man kallar detta för services och de är indelade i tre huvudkategorier:  
* Infrastructure Services, allt som stödjer det som är externt (db, disk, kommunikation mm)  
* Domain Services, allt som stödjer internt inom vår domän  
* Application Services, allt som stödjer applikationen  

Denna del av introt kommer att visa hur man gör ett par Infrastructure services lite mer lättanvända och återanvändbara genom att använda ett begrepp som i NET Core beskrivs som Middleware.  

Att göra om sin kod till att följa NET Core's rekommendationer runt Middleware är egentligen ganska enkelt i grunden. Det handlar om att gömma undan så mycket som möjligt av konkretionen utanför förbrukaren och därigenom uppnå lösa kopplingar, lite grann som ett factory. Domänen/Applikationen behöver inte veta hur man faktiskt kommunicerar via UDP Broadcast, de behöver bara ha ett interface som ger dem en kontaktyta mot denna funktionalitet.  

Genom att dela in sin kod som i detta exempel så uppnår man att applikationerna endast har direkta kopplingar till abstraktionen och till extension/factory metoderna. I koden blir det dessutom mycket snyggare och tydligare/lättläst och man kan implementera funktionaliteten på ett väldigt enkelt sätt!    
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
I koden här ovan ser vi två olika Middleware extensions, **UseServiceTimer** som är en intern extension för att förenkla kodförståelsen och **UseUdpSpeaker** som är en extern extension för att "jacka in" en process på ett enkelt sätt. Båda tar en strängparameter som är namnet på den grupp i konfigurationsfilen som innehåller dess konfiguration.  
  
Som exempel på hur man skriver sina servicar som middleware har jag använt ett grundläggande kommunikationsexempel. Att skicka meddelanden via UDP Broadcast.  

Exemplet är uppdelat i ett antal assemblies och applikationer:  
* Udp.Abstract, all abstraktion för för Udp.Core  
* Udp.Core, all konkretion för processerna  
* Udp.Extensions, extensionmetoder för att implementera UDP i applikationerna  
* Udp.Listener, en BackgroundService som ligger och lyssnar efter UDP Broadcast meddelanden och svarar tillbaka vad den har tagit emot  
* Udp.TimedSender, en BackgroundService som ligger och skickar UDP Broadcast meddelanden  
* Udp.WebSender, en webapplikation som kan skicka UDP Broadcast på server side  

Lägg märke till i Udp.Extensions att det finns en metod som heter **UseUdpSpeaker** och en som heter **AddUdpSpeaker**, skillnaden mellan dessa ligger i att de är extensions för **IHostBuilder** respektive **IServiceCollection**. Innehållet däremot är i princip detsamma. Detta är gjort för att vi ska kunna se att denna typ av extensions kan användas på båda nivåer, vi skiljer på metoderna genom de rekommenderade **Add** och **Use** prefixen.  

Middleware är så otroligt mycket mer än det lilla vi nämnt här, se vidare:  
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/index?view=aspnetcore-3.0

