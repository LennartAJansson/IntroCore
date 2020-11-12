#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Udp.WebSender/Udp.WebSender.csproj", "Udp.WebSender/"]
COPY ["Udp.Abstract/Udp.Abstract.csproj", "Udp.Abstract/"]
COPY ["Udp.Extensions/Udp.Extensions.csproj", "Udp.Extensions/"]
COPY ["Udp.Core/Udp.Core.csproj", "Udp.Core/"]
RUN dotnet restore "Udp.WebSender/Udp.WebSender.csproj"
COPY . .
WORKDIR "/src/Udp.WebSender"
RUN dotnet build "Udp.WebSender.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Udp.WebSender.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Udp.WebSender.dll"]