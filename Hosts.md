# Hosts

Net Core's grund var att p� ett enkelt s�tt skapa ett egenhostat subsystem f�r olika webl�sningar, Web API, Web Applications etc som skulle kunna k�ra plattformsoberoende och selfhosted utan kopplingar till Windows (IIS).  

Ursprungligen s� fanns bara en s� kallad WebHost som med hj�lp av en WebHostBuilder skulle kunna s�tta upp en s�nt h�r system med hj�lp av en massa st�dklasser.  

Man skulle dessutom ha inbyggt st�d f�r IOC och Dependency Injection. Utbyggbarheten skulle hanteras av s� kallade Middlewares, ett f�renklat s�tt att hantera till�ggskomponenter i form av olika servicar som kunde injectas vid behov.  

Efter hand som man vidareutvecklade Net Core ins�g man �ven behovet av att kunna hantera rena serviceapplikationer, s�na som inte hade n�got behov av http och man adderade d�rf�r en GenericHost som byggde p� samma principer men tyv�rr hade lite f�r mycket gemensamt med WebHost. Kort sagt, man hade b�rjat bygga in sig i ett h�rn.  

Inf�r releasen av Net Core 3.0 best�mde man sig f�r att g�ra om allt i grunden och det ledde till att alla former av applikationer numera utg�r ifr�n klassen Host med sin IHostBuilder som finns i Microsoft.Extension.Hosting. Dessa tv� utg�r k�rnan och allt annat �r sen p�byggnader. Vill man ha webrelaterade applikationer s� ut�kar man med Microsoft.AspNetCore.Hosting och l�gger till ett anrop till ConfigureWebHostDefaults d�r man en massa extra funktionalitet som �r unikt f�r webben.  

Se mer: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0  

Att skapa olika webbaserade applikationer �r ganska s� enkelt och v�ldokumenterat s� vad denna solution inneh�ller �r lite mer fokus p� serviceorienterade applikationer. Den inneh�ller exempel p� hur man skapar olika typer av bakgrundsservicar och hur man l�gger till olika typer av funktionalitet i form av middlewares. Den inneh�ller �ven lite tips och tricks runtomkring anv�ndandet av IOC och Dependency Injection.  

I exempel 9 i solution visas hur man skapar sina komponenter som middlewares och hur enkelt det �r sen att l�gga till s�na komponenter i b�de servicebaserade applikationer s�v�l som i webbaserade. I detta exempel ser man ocks� hur genomt�nkt middlewarekonceptet �r d� det uppmuntrar till Dependency Injection och hur den anv�nder extensions som en f�renklad form av Factory f�r att skapa l�sa bindningar med hj�lp av abstraktion. Det �r s�n kod man blir glad av att l�sa och det �r s�n kod som uppmuntrar till att skriva tester f�r! :)  

I varje solutionfolder s� finns det �ven ett markdowndokument, alla har inte inneh�ll �nnu men tanken �r att l�gga till lite kort fakta och eventuella l�nkar till ytterligare information till det som avsnittet behandlar. D�rigenom f�r vi en liten manual i hur man �stadkommer olika saker med nya Net Core 3.0 och C#8. Fyll g�rna p� med kommentarer och exempel men f�rs�k f�lja samma struktur och layout som redan finns i solutionen.

