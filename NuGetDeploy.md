# NuGet Deploy

N�r bygget �r provk�rt och fungerar s� kan man direkt fr�n ett k�rt bygge v�lja att skapa en Release. Detta kommer att �ppna en dialog d�r man kan v�lja en Template, l�ngst upp finns en l�nk som det st�r Or start with an Empty job. V�lj detta alternativ!  

N�sta steg �r att man fr�gas om namnet p� sin f�rsta Stage, en stage �r ungef�r motsvarande en milj� och vad som ska h�nda i denna milj�, kalla den bara ArtifactFeed och st�ng sedan den fliken p� krysset.  

I rutan f�r ArtifactFeed stage ser det nu ut s� h�r:

<img src="ArtifactStage.png" width="400"/>  

Klicka p� l�nken d�r det st�r "0 task" s� kommer f�ljande vy fram:  

<img src="TaskView.png" width="400"/>

Klicka p� plussymbolen till h�ger om Agent Job s� kommer n�sta vy fram i h�gerkanten:  

<img src="AddNuGetTask.png" width="400"/>

S�k som bilden visar efter ordet nuget och v�lj Add p� den �versta Task som heter enbart NuGet. Fyll i den med f�ljande information:  

<img src="NuGetTask.png" width="400"/>

Command ska vara Push, Path to NuGet packages to publish ska vara $(System.DefaultWorkingDirectory)/Intro/drop/**/*.nupkg d�r Intro �r namnet p� byggdefinitionen. Man kan bl�ddra sig dit om man �r os�ker men gl�m inte att l�gga till /**/*.nupkg i slutet.  
Target feed location ska vara This Organization/Collection och p� Target Feed v�ljs den Feed som vi skapade i f�reg�ende dokument, i detta exempel var det IntroNew.  

Klicka slutligen p� Pieline uppe under All Pipelines s� f�ljande vy visas:  

<img src="DeployTrigger.png" width="400"/>  

Klicka p� blixtsymbolen i rutan med Artifacts och se till att Continuous Deployment �r aktiverat.  

Nu ska allt vara klart och det kan vi testa genom att starta ett bygge, om det k�r gr�nt s� kommer en release att skapas automatiskt och en deploy kommer att startas till Artifact Feed. Om allt blir gr�nt s� kan man verifiera detta genom att g� in i Artifact och kolla om paketet g�r att hitta!  