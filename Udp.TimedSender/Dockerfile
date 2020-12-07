#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster AS build
WORKDIR /src
COPY ["Udp.TimedSender/Udp.TimedSender.csproj", "Udp.TimedSender/"]
COPY ["Udp.Extensions/Udp.Extensions.csproj", "Udp.Extensions/"]
COPY ["Udp.Abstract/Udp.Abstract.csproj", "Udp.Abstract/"]
COPY ["Udp.Core/Udp.Core.csproj", "Udp.Core/"]
RUN dotnet restore "Udp.TimedSender/Udp.TimedSender.csproj"
COPY . .
WORKDIR "/src/Udp.TimedSender"
RUN dotnet build "Udp.TimedSender.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Udp.TimedSender.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Udp.TimedSender.dll"]