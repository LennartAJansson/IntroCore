# NuGet Build  

Under repot som ska ha byggautomation klickar man på Setup Build, välj en tom definition i yaml och fyll i följande:  
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
#Säkerställ att rätt NET Core SDK finns
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
#Kör eventuella unittester
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

Vad denna byggdefinition gör är att den kommer att använda Net Core CLI för att kompilera och samtidigt versionera både binärer såväl som NuGet paketen. Allt byggs och testas i Release och efter lyckat bygge så kopieras NuGet paketen över till ArtifactStagingDirectory och publiceras slutligen som en byggartifakt som vi kan hämta in i en deploy pipeline och placera i en artifact feed.  

Fortsätt nu till nästa dokument där vi skapar ett Artifact Feed som vi kan deploya till.
[Artifact Feed](Artifact.md)
