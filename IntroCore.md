# NET Core 3.x

I denna solution kommer jag att gå igenom hur man utvecklar med hjälp av NET Core och framförallt hur man använder sig av Microsoft NET Core Hosting Framework. Denna framework kallas även Microsoft Extensions och innehåller allt för att du ska kunna utveckla smarta applikationer som dels följer "SOLID principles" men som även tillämpar många av de "Principle and patterns" som skapades av "The gang of four".  

Varje exempel kommer oftast att utgå från tidigare exempel i denna solution och tillföra ytterligare funktionalitet steg för steg.  

För att kunna köra alla exempel behövs ett antal komponenter tillgängliga:

* MSDN abbonemang

* Azure Devops med ett repo för denna solution

* Visual Studio 2019

* NET Core 3.x

Jag kommer även att presentera build och release pipelines i det exempel som tar upp hur man skapar egna nuget-paket. Detta för att du ska se helhetsbilden i hur man skapar och använder dessa paket.  

## NET Cores historia

När man skapade NET Core hade man som huvudmål att uppnå plattformsoberoende, "one framework to rule them all". Man ville skapa en bas som kunde köra lika bra under Linux, IOS såväl som Windows.  

Man hade även som mål att förenkla och generalisera hur vi skriver web applications, plattformsoberoendet framtvingade detta eftersom Internet Information Service, IIS, som NET Framework förlitat sig på inte är en standardkomponent i Linux eller IOS.  

Dagens och framtiden arkitektur siktar väldigt mycket mot containers och mot molnbaserade tjänster och detta var också en tungt vägande faktor för ett nytt framework.

Med i planeringen fanns även att göra ett framework som var byggt på "SOLID principle" och Principle and patterns" dvs stöd för Single Responsibility, Inversion Of Control och Dependency Injection med hjälp av så kallade Factories och Builders.  

Tyvärr insåg man ganska tidigt att man hade byggt in sig i en hörna, man hade baserat det hela för mycket runt Web Applications och gjort det svårare för all annan typ av utveckling. Med en framtida plan för att flytta över WPF, WinForms och Windows Services NET Core för att sedan kunna lägga ner NET Framework så behövde NET Core förändras fundamentalt och detta skedde i samband med utgivningen av version 3.x.

NET Framework är således på väg att fasas ut helt, i samband med att man släpper release 5.x så kommer det bara finnas en renodlad NET Core produkt. I och med detta så får vi ett framework som är mycket bättre arkitekturellt och mer genomtänkt i sin utbyggbarhet. Det är ett framework som underlättar för oss utvecklare att skriva bra och solid kod!

##  Hosts

Själva grunden i NET Cores så kallade Hosting Framework är en Host eller dess abstraction IHost, denna Host innehåller en massa fundamentala abstraktioner som IConfiguration, IServiceProvider m fl.  

För att kunna hantera dessa med lösa kopplingar för att på ett enkelt sätt kunna byta ut implementationerna av dessa abstraktioner så använder man IOC, Inversion Of Control. 

Runtomkring hela denna IOC behållare, som i NET Core kallas en ServiceProvider, så finns det en massa olika Factories och Builders. Kortfattat så kan man säga att vi registrerar Factories tillhörande en specifik Builder och när vi sedan bygger vår Builder så skapas det en massa registreringar i vår ServiceProvider som i sin tur hanterar alla efterfrågade injections i constructors och properties.  

I slutändan så strävar vi efter att vi aldrig ska använda oss själva av new i vår programkod för att skapa ett objekt utan vi frågar ServiceProvider efter en implementering av ett specifikt interface. ServiceProvider som känner alla dessa relationer svarar tillbaka med en implementation (ett objekt) och kollar samtidigt om denna implementation vill ha ytterligare implementationer injectade i sin construktor.

I slutändan när alla Factories, Builders och abstraktioner/implementationer har definierats så anropar man metoden Build på den yttersta Buildern. Denna Builder är alltid en IHostBuilder och Build-metoden kommer att konstruera allting som behövs i en IHost och vi kan därefter exekvera applikationen genom att anropa t ex Run eller RunAsync på denna IHost.

Saker som sköts automatiskt av IHostBuilder och som vi inte behöver skriva kod för själva är t ex inläsning av konfigurationen. Detta kommer ske under Build-fasen och den kommer att läsa in appsettings.json, appsettings.Development.json, Environmentvariabler och kommandorad som standard och den kommer att abstrahera allt detta som en gemensam IConfiguration.

## Läsa enbart IConfiguration utan någon Host

Nu kommer vi till det första exemplet och här ska vi titta lite på en av byggstenarna i Hosting-konceptet, den som hanterar konfigurationer. I exemplet utgår vi från en helt vanlig NET Core Console Application.

Börja med att lägga till de två appsettings-filer som finns i exemplet, de ska se ut som följer:

<u>appsettings.Development.json</u>

```json
{
  "DevelopmentGroup": {
    "DevelopmentValue": "This is a development value"
  }
}
```

<u>appsettings.json</u>

```json
{
  "GlobalGroup": {
    "GlobalValue": "This is a global value"
  }
}
```

Glöm inte att markera att dessa filer ska följa med till build output folder. 

Sedan lägger vi till referenser till följande Nuget-paket:

```c#
Microsoft.Extensions.Configuration
Microsoft.Extensions.Configuration.CommandLine
Microsoft.Extensions.Configuration.EnvironmentVariables
Microsoft.Extensions.Configuration.Json
```

<u>Program.cs</u>

```c#
class Program
{
    static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, 
                         reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true,
                         reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        Console.WriteLine("You now have access to a complete IConfiguration through variable configuration");
        Console.WriteLine($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
        Console.WriteLine($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
        Console.WriteLine($"  {configuration.GetSection("EnvGroup")["EnvValue"]}");
        Console.WriteLine($"  {configuration.GetSection("CmdGroup")["CmdValue"]}");
    }
}
```

Lägg märke till att vi enbart behöver lägga till en using för Microsoft.Extensions.Configuration men vi behöver däremot lägga till referenser till de tre som hanterar respektive typ av konfiguration. Det är på grund av att den extensionmetod för ConfigurationBuilder som heter AddJsonFile finns i Microsoft.Extensions.Configuration.Json och den extensionmetod som heter AddEnvironmentVariables finns i Microsoft.Extensions.Configuration.EnvironmentVariables samt den extensionmetod som heter AddCommandLine finns i Microsoft.Extensions.Configuration.CommandLine. Dessa extensionmetoder finns alltså på annan plats, i en annan class library, men de ligger under namespacet Microsoft.Extensions.Configuration i dessa class libraries. 

Detta är alltså inte metoder som finns med från början i ConfigurationBuilder utan det är som vi utökar funktionalitet på denna Builder med hjälp av extensions, det är så NET Core Hosting är uppbyggt i grunden.     

Vi kan lätt bygga sådana extensionmetoder själva för alla builders som finns och det är rekommenderad best practice från Microsoft att göra på detta viset. Man skapar bara en extensionmetod som ligger i samma namespace som den builder den ska utöka funktionen på men fysiskt kan denna extensionmetod till och med ligga i en annan class library och vi behöver i så fall bara lägga till en referens till detta class library så hittas vår extension automatiskt.

Om vi sen tittar på de sista raderna, där vi skriver ut värden som vi läser från vår konfiguration så ser vi att detta hanteras genom att vi använder en metod som heter GetSection i vår byggda IConfiguration:

```configuration.GetSection("GlobalGroup")["GlobalValue"]```

Relatera detta till hur det faktiskt ser ut i appsettings.json så ser du tydligt hur detta värde adresseras. Intressant är att jag skulle lika gärna kunna använda :

```configuration.GetValue<string>("GlobalGroup:GlobalValue")```

Funktionen blir densamma! Sen är det viktigt att komma ihåg att IConfiguration gör ingen skillnad på varifrån värdena kommer ifrån, jag skulle kunna göra så här:

```configuration.GetValue<string>("Username")``` 

Vilket innebär att den hämtar Username från Environment!

Om man nu tänker efter så inser man att det finns en tänkbar konfliktsituation här, tänk om jag har ett värde i en appsettingsfil som heter just Username, vilken väljer den då? Jo, det hänger ihop med hur vi definierar våra olika konfigurationer för ConfigurationBuilder, i vårt fall kommer den att läsa appsettings.json först, sen kommer den att läsa appsettings.Development.json och om den senare innehåller ett likadant värde så kommer dess värde att överrida den tidigares. På samma vis går det vidare genom hela kedjan och då inser vi att man kan alltid överrida tidigare värden med nästa ConfigurationProviders motsvarande värde och därför kan man säga att commandline har högsta prioritet, environment näst högsta, appsettings.Development.json står näst i tur och sist i kedjan kommer appsettings.json.

En liten sak till innan vi lämnar IConfiguration för denna gången. Om vi nu tänker oss att vi har ett värde i appsettings.json som i vårat exempel, GlobalGroup:GlobalValue, hur skulle vi skrivit det som en commandline eller som en environment?

Vi kan inte använda kolon i environment men om vi ersätter kolon med dubbla underscore (_) så kommer dess ConfigurationProvider att tolka det på rätt sätt. Alltså blir environmentvärdet för samma värde: 

```EnvGroup__EnvValue="This is a value from environment"```

I fallet med commandline så är det lättare, där är kolon accepterat och vi kan deklarera det som:

```CmdGroup:CmdValue="This is a value from commandline"```

Titta i exemplet, värdena för environment och commandline hittar du under projektets Properties på fliken Debug.

## Använda dependency injection (DI) utan någon Host

Detta exempel bygger vidare på föregående och utgår från att vi skapar vår IConfiguration och gör så vi kan injecta denna i två andra klasser.

För att få DI att fungera så behöver vi lägga till ytterligare en referens utöver de från förra exemplet som hanterade konfigurationen:

```c#
Microsoft.Extensions.DependencyInjection
```

Det är denna extension som hanterar allt runt IOC och DI, IOC behållaren kallas i detta fallet för en ServiceProvider med sitt interface IServiceProvider. När det gäller att bygga upp en ServiceProvider med registreringar så har vi flera alternativ att ta till, först måste får vi bestämma vad det är vi ska registrera och sedan hur detta ska skapas när det efterfrågas. 

I vårt exempel så skapar jag först en ServiceCollection som är vår Builder för en ServiceProvider. I denna ServiceCollection lägger jag till en vanlig publik klass som heter DIUserClass med metoden ```AddScoped<DIUSerClass>()``` det innebär att jag har registrerat DIUserClass så att min ServiceProvider kommer att känna till den, den kommer att leva Scoped dvs för varje gång den efterfrågas så kommer ServiceProvider skapa ett nytt objekt, detta är den kortaste livslängden vi har på objekt som skapas med DI. 

Skillnaden med livscykel ser man om man i stället använder ```AddTransient<DIUserClass>()```, då kan objektet delas av flera brukare som efterfrågar ett objekt av den typen under livscykeln av den första skapade. Slutligen så kan jag registrera den med ```AddSingleton<DIUserClass>()```, då kommer objektet skapas första gången det efterfrågas och sedan leva kvar i Hostens livslängd.

När jag sen efter alla registreringarna anropar metoden BuildServiceProvider så kommer en ServiceProvider att skapas som jag kan använda för att hämta nya objekt, den kan även ansvara helt för att räkna ut vilka interface/objekt som behöver injectas vid skapandet av de olika objekten.

För att hämta ett objekt eller ett interface till ett object så anropar man ServiceProviderns metod GetService och anger vilket interface eller objekt man vill ha och då skapar ServiceProvidern detta.

I vårt fall skapar vi en instans av DIUserClass och ServiceProvider ser att den då behöver injecta en IConfiguration i DIUserClass tillsammans med ett interface av typen ITestService. När den försöker skapa en implementering av ITestService så upptäcker den att klassen TestService dessutom behöver ha en IConfiguration så då injectas den dit också.

Resultatet blir att vår Program känner till sin ServiceProvider men inget om den implementerade klassen TestService. Vi anropar GetService<DIUserClass>() och på detta objekt anropar vi RunAsync() och så skapar vi en awaiter för att vänta på att RunAsync exekverat klart.

För att rätta kompileringsfel måste följande usings läggas till i Program.cs:

* System

* System.Threading.Tasks

* Microsoft.Extensions.Configuration

* Microsoft.Extensions.DependencyInjection

<u>Program.cs</u>

```c#
class Program
{
    static void Main(string[] args)
    {
        //Create the IConfiguration
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, 
                         reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true,
                         reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        //Create an IServiceProvider and register three different scopes
        var serviceProvider = new ServiceCollection()
            .AddScoped<DIUserClass>()
            .AddScoped<ITestService, TestService>()
            .AddSingleton<IConfiguration>(configuration)
            .BuildServiceProvider();

        Console.WriteLine("You now have access to a complete IServiceProvider (IOC) through variable serviceProvider");

        //Use the serviceprovider to create an object of DIUserClass
        //Then execute it and wait for it to finish
        serviceProvider
            .GetService<DIUserClass>()
            .RunAsync()
            .GetAwaiter();
    }
}
```

DIUserClass har som sagt injection av ITestService och IConfiguration och i metoden RunAsync så skriver vi ut lite från konfigurationen och sen anropar vi metoden ExecuteAsync i implementationen av ITestService.  Eftersom RunAsync är async så kan vi använda await för att vänta på att ExecuteAsync kört färdigt.

<u>DIUserClass.cs</u>

```c#
class DIUserClass
{
    private readonly ITestService testService;
    private readonly IConfiguration configuration;

    public DIUserClass(ITestService testService, IConfiguration configuration)
    {
        this.testService = testService;
        this.configuration = configuration;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("DIUserClass.RunAsync...");
        Console.WriteLine($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
        Console.WriteLine($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
        Console.WriteLine($"  {configuration.GetSection("EnvGroup")["EnvValue"]}");
        Console.WriteLine($"  {configuration.GetSection("CmdGroup")["CmdValue"]}");

        await testService.ExecuteAsync();
    }
}
```

ITestService är bara en service abstraktion som föreskriver att en implementation av interfacet måste innehålla en metod som heter ExecuteAsync. Vi vet överhuvudtaget inte vad ITestService är implementerat av för klass och vi behöver ju inte veta det så länge den klassen implementerar interfacets metod ExecuteAsync.

<u>ITestService.cs</u>

```c#
interface ITestService
{
	Task ExecuteAsync();
}
```

TestService är implementationen av ITestService och den har dessutom en constructor som injectar en IConfiguration. Därigenom får vi tillgång till konfigurationen även i denna klass och kan skriva ut lite statustext när ExecuteAsync anropas. Eftersom ExecuteAsync inte har något att vänta på med await så returnerar vi en Task.CompletedTask när vi avslutar metoden.

<u>TestService.cs</u>

```c#
class TestService : ITestService
{
    private readonly IConfiguration configuration;

    public TestService(IConfiguration configuration) => this.configuration = configuration;

    public Task ExecuteAsync()
    {
        Console.WriteLine("TestClass.ExecuteAsync...");
        Console.WriteLine($"  {configuration.GetSection("GlobalGroup")["GlobalValue"]}");
        Console.WriteLine($"  {configuration.GetSection("DevelopmentGroup")["DevelopmentValue"]}");
        Console.WriteLine($"  {configuration.GetSection("EnvGroup")["EnvValue"]}");
        Console.WriteLine($"  {configuration.GetSection("CmdGroup")["CmdValue"]}");

        return Task.CompletedTask;
    }
}
```

## Använda loggning med hjälp av ILogger

Så här långt har vi använt oss av Console.WriteLine för att göra vår output på skärmen. Det rätta sättet i detta koncept är egentligen att använda en så kallad ILogger istället. ILogger är ett interface som gör det möjligt för oss att använda lite mer avancerad loggning till Console, File, Eventlog, Databas, Application Insight mm mm. Vi registrerar bara vilken implementation som ska användas av ILogger.

För att få igång denna ILogger så behöver vi lägga till referenser till den extension som hanterar loggningen samt referenser till de sätt som vi vill logga med. Jämför med hur vi gjorde med konfigurationen, principen är densamma. Lägg till referenser till:

```c#
 Microsoft.Extensions.Logging
 Microsoft.Extensions.Logging.Console
```

 I appsettings.json lägger du till loggningskonfigurationen så det ser ut så här:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "GlobalGroup": {
    "GlobalValue": "This is a global value"
  }
}
```

Om du tittar på den gruppen som heter Logging så innehåller den en undergrupp som heter LogLevel. Varje värde i LogLevel motsvarar ett namespace, vi har ett värde som heter Default som säger att i normala fall så logga inget som är av lägre prioritet är Information. Det innebär att by default så loggar den inget av Debug eller Trace prioritet. Om den klass vi loggar från däremot tillhör namespacet Microsoft så loggar den inget med lägre prioritet än Warning men om namespacet är Microsoft.Hosting.Lifetime så loggar den inget mindre än Information. På detta viset styr vi vilka prioritet vi vill ha på loggningen i våra olika klasser.

Tänk på vad jag sagt tidigare om konfigurationens prioritetsordningar, om du lägger något annorlunda under Logging i din appsettings.Development.json så kommer det att överrida det som är angivet i appsettings.json!

Ok, det var konfigurationen av loggningen, hur använder vi nu detta?

För det första måste du lägga till en using till ```Microsoft.Extensions.Logging``` i din Program.cs och alla andra klasser där du ska använda loggningen.

Sen ändrar du bara de raderna där du skapar din ServiceProvider, den ska i stället se ut så här:

```c#
var serviceProvider = new ServiceCollection()
    .AddLogging(loggingBuilder =>
    	loggingBuilder.AddConsole())
    .AddScoped<DIUserClass>()
    .AddScoped<ITestService, TestService>()
    .AddSingleton<IConfiguration>(configuration)
    .BuildServiceProvider();
```

De enda rader som har tillkommit är AddLogging(loggingBuilder => loggingBuilder.AddConsole())

Som du ser så använder vi en extension för ServiceCollection som heter AddLogging, denna har en action i form av en ILoggingBuilder som vi använder för att anropa en extensionmetod till den som heter AddConsole.