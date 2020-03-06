# Hosts

Net Core's grund var att på ett enkelt sätt skapa ett egenhostat subsystem för olika weblösningar, Web API, Web Applications etc som skulle kunna köra plattformsoberoende och selfhosted utan kopplingar till Windows (IIS).  

Ursprungligen så fanns bara en så kallad WebHost som med hjälp av en WebHostBuilder skulle kunna sätta upp en sånt här system med hjälp av en massa stödklasser.  

Man skulle dessutom ha inbyggt stöd för IOC och Dependency Injection. Utbyggbarheten skulle hanteras av så kallade Middlewares, ett förenklat sätt att hantera tilläggskomponenter i form av olika servicar som kunde injectas vid behov.  

Efter hand som man vidareutvecklade Net Core insåg man även behovet av att kunna hantera rena serviceapplikationer, såna som inte hade något behov av http och man adderade därför en GenericHost som byggde på samma principer men tyvärr hade lite för mycket gemensamt med WebHost. Kort sagt, man hade börjat bygga in sig i ett hörn.  

Inför releasen av Net Core 3.0 bestämde man sig för att göra om allt i grunden och det ledde till att alla former av applikationer numera utgår ifrån klassen Host med sin IHostBuilder som finns i Microsoft.Extension.Hosting. Dessa två utgör kärnan och allt annat är sen påbyggnader. Vill man ha webrelaterade applikationer så utökar man med Microsoft.AspNetCore.Hosting och lägger till ett anrop till ConfigureWebHostDefaults där man en massa extra funktionalitet som är unikt för webben.  

Se mer: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0  

Att skapa olika webbaserade applikationer är ganska så enkelt och väldokumenterat så vad denna solution innehåller är lite mer fokus på serviceorienterade applikationer. Den innehåller exempel på hur man skapar olika typer av bakgrundsservicar och hur man lägger till olika typer av funktionalitet i form av middlewares. Den innehåller även lite tips och tricks runtomkring användandet av IOC och Dependency Injection.  

I exempel 9 i solution visas hur man skapar sina komponenter som middlewares och hur enkelt det är sen att lägga till såna komponenter i både servicebaserade applikationer såväl som i webbaserade. I detta exempel ser man också hur genomtänkt middlewarekonceptet är då det uppmuntrar till Dependency Injection och hur den använder extensions som en förenklad form av Factory för att skapa lösa bindningar med hjälp av abstraktion. Det är sån kod man blir glad av att läsa och det är sån kod som uppmuntrar till att skriva tester för! :)  

I varje solutionfolder så finns det även ett markdowndokument, alla har inte innehåll ännu men tanken är att lägga till lite kort fakta och eventuella länkar till ytterligare information till det som avsnittet behandlar. Därigenom får vi en liten manual i hur man åstadkommer olika saker med nya Net Core 3.0 och C#8. Fyll gärna på med kommentarer och exempel men försök följa samma struktur och layout som redan finns i solutionen.

