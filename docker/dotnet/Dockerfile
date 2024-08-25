FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ./Geo/ .
WORKDIR "/src/."

RUN dotnet restore ./src/Geo.Api/Geo.Api.csproj
RUN dotnet restore ./src/Geo.DataSeeding/Geo.DataSeeding.csproj

RUN dotnet tool install -g dotnet-ef
#RUN /root/.dotnet/tools/dotnet-ef database update -s .\Geo\src\Geo.Api -p .\Geo\src\Geo.DataAccess   

RUN dotnet build "./src/Geo.Api/Geo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/buildapi
RUN dotnet build "./src/Geo.DataSeeding/Geo.DataSeeding.csproj" -c $BUILD_CONFIGURATION -o /app/buildcli
RUN find /app/buildapi -type d -print0|xargs -0 chmod ugo+w
RUN find /app/buildcli -type d -print0|xargs -0 chmod ugo+w


RUN echo "sed -i 's/__GeoApiDbContext__: __.*__/__GeoApiDbContext__: __'__###ConnectionStrings__'__/'  /app/buildcli/appsettings.json" > /app/buildcli/migrationrun0.sh \
	&& echo "sed -i 's/__GeoApiDbContext__: __.*__/__GeoApiDbContext__: __'__###ConnectionStrings__'__/'  /app/buildapi/appsettings.Development.json" >> /app/buildcli/migrationrun0.sh \
	&& sed -i 's/__/"/g' /app/buildcli/setconnectstring.sh \
	&& sed -i 's/###/\$/g' /app/buildcli/setconnectstring.sh \
	&& chmod ugo+x /app/buildcli/setconnectstring.sh 


RUN echo "#!/bin/bash\n" > /app/buildcli/migrationrun.sh \
	&& echo "eval __/root/.dotnet/tools/dotnet-ef database update -s /src/src/Geo.Api --connection \__###ConnectionStrings\____" >> /app/buildcli/migrationrun.sh \
	&& sed -i 's/__/"/g' /app/buildcli/migrationrun.sh \
	&& sed -i 's/###/\$/g' /app/buildcli/migrationrun.sh \
	&& chmod ugo+x /app/buildcli/migrationrun.sh 

WORKDIR /app/buildcli/.
#RUN /app/buildcli/Geo.DataSeeding

#RUN apt-get update 
#RUN yes | apt-get install nginx
#RUN apt-get install nginx -y

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
