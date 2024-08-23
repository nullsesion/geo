FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ./Geo/src .
WORKDIR "/src/."

RUN dotnet restore ./Geo.Api/Geo.Api.csproj
RUN dotnet restore ./Geo.DataSeeding/Geo.DataSeeding.csproj

RUN dotnet tool install -g dotnet-ef
#RUN /root/.dotnet/tools/dotnet-ef database update -s .\Geo\src\Geo.Api -p .\Geo\src\Geo.DataAccess   

RUN dotnet build "./Geo.Api/Geo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/buildapi
RUN dotnet build "./Geo.DataSeeding/Geo.DataSeeding.csproj" -c $BUILD_CONFIGURATION -o /app/buildcli

WORKDIR /app/buildcli/.
#RUN /app/buildcli/Geo.DataSeeding

#RUN apt-get update 
#RUN yes | apt-get install nginx

#todo: make a clean build in the future 
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./WebApplication1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .

WORKDIR /app/buildapi/.
ENTRYPOINT ["dotnet", "/app/buildapi/Geo.Api.dll"]
#CMD ["nginx", "-g", "daemon off;"]
