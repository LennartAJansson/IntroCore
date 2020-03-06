# BackgroundService  

F�r att f�renkla hanteringen av hosted services har Microsoft f�rsett oss med  en egen implementation av IHostedService som heter BackgroundService. Den tillf�r f�ljande:  
  
* En egen implementation av StartAsync och StopAsync med inbyggd hantering av en s� kallad CancellationToken vilket inneb�r att man kan signaleras om avbrott och d�rigenom agera utifr�n denna signalering.  
* Konstruktorn b�r, om man har en egen implementation, �ven anropa basklassens konstruktor f�r att detta ska aktiveras.  
* StartAsync och StopAsync g�r att overrida men man b�r �ven d�r anropa basklassens implementation s� all hantering runt cancellations sker som t�nkt.  
* Klassen implementerar �ven IDisposable och har en overridable Dispose-metod som �ven den b�r anropas i basklassen.
* Ut�ver detta s� har den �ven en abstrakt metod som heter ExecuteAsync, denna m�ste man ha en egen implementation f�r.  
  
F�r att g�ra detta exempel s� kan man anv�nda en annan nyhet n�r det g�ller bakgrundsservicar:  
Det finns en template och en SDK f�r att f�renkla skapandandet av denna typ av servicar. Om man v�ljer att skapa ett nytt projekt och s�ker efter Worker i add-dialogen s� finns det en f�rdig projekttemplate f�r att skapa en console-applikation med en grundl�ggande implementation av IHostedService i en BackgroundService.  
Den ger dig dessutom konfigurationsfiler och dependency injection av en ILogger, det inneb�r att vi kan anv�nda det interfacets metoder LogInformation, LogWarning och LogError f�r output i st�llet f�r Console.WriteLine. Vi kommer p� s� vis ett steg n�rmare en logghantering som kan kopplas till fil, eventlog, databas eller annan persistering av loggen.  

