#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Udp.Listener/Udp.Listener.csproj", "Udp.Listener/"]
COPY ["Udp.Extensions/Udp.Extensions.csproj", "Udp.Extensions/"]
COPY ["Udp.Abstract/Udp.Abstract.csproj", "Udp.Abstract/"]
COPY ["Udp.Core/Udp.Core.csproj", "Udp.Core/"]
RUN dotnet restore "Udp.Listener/Udp.Listener.csproj"
COPY . .
WORKDIR "/src/Udp.Listener"
RUN dotnet build "Udp.Listener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Udp.Listener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Udp.Listener.dll"]