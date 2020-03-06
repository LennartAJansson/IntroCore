# BackgroundService  

För att förenkla hanteringen av hosted services har Microsoft försett oss med  en egen implementation av IHostedService som heter BackgroundService. Den tillför följande:  
  
* En egen implementation av StartAsync och StopAsync med inbyggd hantering av en så kallad CancellationToken vilket innebär att man kan signaleras om avbrott och därigenom agera utifrån denna signalering.  
* Konstruktorn bör, om man har en egen implementation, även anropa basklassens konstruktor för att detta ska aktiveras.  
* StartAsync och StopAsync går att overrida men man bör även där anropa basklassens implementation så all hantering runt cancellations sker som tänkt.  
* Klassen implementerar även IDisposable och har en overridable Dispose-metod som även den bör anropas i basklassen.
* Utöver detta så har den även en abstrakt metod som heter ExecuteAsync, denna måste man ha en egen implementation för.  
  
För att göra detta exempel så kan man använda en annan nyhet när det gäller bakgrundsservicar:  
Det finns en template och en SDK för att förenkla skapandandet av denna typ av servicar. Om man väljer att skapa ett nytt projekt och söker efter Worker i add-dialogen så finns det en färdig projekttemplate för att skapa en console-applikation med en grundläggande implementation av IHostedService i en BackgroundService.  
Den ger dig dessutom konfigurationsfiler och dependency injection av en ILogger, det innebär att vi kan använda det interfacets metoder LogInformation, LogWarning och LogError för output i stället för Console.WriteLine. Vi kommer på så vis ett steg närmare en logghantering som kan kopplas till fil, eventlog, databas eller annan persistering av loggen.  

