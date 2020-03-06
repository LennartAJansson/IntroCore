# NuGet Build  

Under repot som ska ha byggautomation klickar man p� Setup Build, v�lj en tom definition i yaml och fyll i f�ljande:  
```
trigger:
- master

pool:
  vmImage: 'ubuntu-latest'

name: '$(majorMinorVersion).$(semanticVersion)' 

variables:
  buildConfiguration: 'Release'
  majorMinorVersion: 2.0.0
  semanticVersion: $[counter(variables['majorMinorVersion'], 0)]
  buildVersion: '$(majorMinorVersion).$(semanticVersion)'  

steps:
#S�kerst�ll att r�tt NET Core SDK finns
- task: UseDotNet@2
  displayName: 'Use .Net Core sdk 3.1.x'
  inputs:
    version: 3.1.x
#Kompilera hela repot
- task: DotNetCoreCLI@2
  inputs:
    command: 'build'
    projects: '**/*.csproj'
    arguments: '--configuration $(buildConfiguration) -p:Version=$(buildVersion)'
#K�r eventuella unittester
- task: DotNetCoreCLI@2
  inputs:
    command: 'test'
    projects: '**/*.UnitTests.csproj'
    arguments: '--configuration $(buildConfiguration) -p:Version=$(buildVersion)'
    testRunTitle: 'Unittests $(Build.DefinitionName)_$(buildVersion)'
#Kopiera Nuget-paket till ArtifactStagingDirectory
- task: CopyFiles@2
  inputs:
    Contents: '**/bin/$(buildConfiguration)/**/*.nupkg'
    TargetFolder: '$(Build.ArtifactStagingDirectory)'
#Publicera ArtifactStagingDirectory till droplocation
- task: PublishBuildArtifacts@1
  inputs:
    PathtoPublish: '$(Build.ArtifactStagingDirectory)'
    ArtifactName: 'drop'
    publishLocation: 'Container'
```  

Vad denna byggdefinition g�r �r att den kommer att anv�nda Net Core CLI f�r att kompilera och samtidigt versionera b�de bin�rer s�v�l som NuGet paketen. Allt byggs och testas i Release och efter lyckat bygge s� kopieras NuGet paketen �ver till ArtifactStagingDirectory och publiceras slutligen som en byggartifakt som vi kan h�mta in i en deploy pipeline och placera i en artifact feed.  

Forts�tt nu till n�sta dokument d�r vi skapar ett Artifact Feed som vi kan deploya till.
[Artifact Feed](Artifact.md)
