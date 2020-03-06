# NuGet Paket  

I det f�rra exemplet om middlewares s�g vi hur man kan skapa middlewares av �teranv�ndbara komponenter. F�r att kunna �teranv�nda dessa komponenter s� beh�ver man inkludera Udp.Abstract, Udp.Core and Udp.Extension i sin solution p� n�got vis.  

Det absolut l�ttaste s�ttet att hantera detta p� �r att skapa NuGet paket utav var och en av dessa tre komponenter. Genom detta s� f�r vi en tydlig leverabel f�r komponenterna som �r enkel att versionshantera, uppdatera och inkludera.  

Att skapa NuGet paket har blivit otroligt enkelt i Net Core, det r�cker med att man �ppnar egenskaperna f�r de projekt man vill g�ra NuGet paket av och g�r till fliken som heter Publish, d�r har man en checkbox som heter Generate NuGet package on build. Fyll i informationen som i nedanst�ende bild:  

![Nu Get Publish](NuGetPublish.png)  

Efter detta kommer varje kompilering att generera ett NuGet paket (.nupkg) av detta projekt under bin\Debug eller bin\Release, beroende p� vilket man valt att kompilera. Paketet inneh�ller all information om dependencies mm till andra paket och om man installerar ett s�dant paket s� kommer det automatiskt att dra ner alla andra paket den beh�ver.  

I v�rt exempel beh�ver vi som sagt g�ra NuGet paket av de tre projekten Udp.Abstract, Udp.Core och Udp.Extensions och vi beh�ver bara l�gga till paketet Udp.Extensions till v�r solution d�r vi vill inkludera denna middleware. Om vi har n�got projekt d�r vi bara injektar en IUdpListener eller IUdpSpeaker fr�n IOC s� r�cker det att vi l�gger till Udp.Abstract till detta projekt.  

N�sta steg �r att automatisera tillverkningen av dessa paket och d� flyttar vi �ver till Azure DevOps, detta beskrivs i n�sta dokument, [NuGet Build](NuGetBuild.md)  

H�r finns ett exempel som �r ungef�r likadant, enda skillnaden �r att man i det exemplet deployar sitt NuGet paket som en del av Build vilket �r strategiskt fel att g�ra. Build �r till f�r att kompilera och testa, inget annat. Deploy �r ett steg i en release process och vi b�r kunna hantera att deploya olika states av paketen till olika NuGet feeds. Vidare s� inneh�ller denna websida inget om hur man versionerar sina bin�rer och NuGet paket! V�r beskrivning d�remot inneh�ller allt detta.  
https://medium.com/@dan.cokely/creating-nuget-packages-in-azure-devops-with-azure-pipelines-and-yaml-d6fa30f0f15e
