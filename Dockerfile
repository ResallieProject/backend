#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 3001
ENV ASPNETCORE_URLS=http://+:3001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Resallie.csproj", "."]
RUN dotnet restore "./Resallie.csproj"

COPY . .
WORKDIR "/src/."
RUN dotnet build "Resallie.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Resallie.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Resallie.dll"]