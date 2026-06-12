FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ./Geo/ .

RUN dotnet restore ./src/Geo.Api/Geo.Api.csproj
RUN dotnet restore ./src/Geo.DataSeeding/Geo.DataSeeding.csproj

RUN dotnet tool install -g dotnet-ef

RUN dotnet build "./src/Geo.Api/Geo.Api.csproj" -c Release -o /app/api
RUN dotnet build "./src/Geo.DataSeeding/Geo.DataSeeding.csproj" -c Release -o /app/cli

RUN echo "#!/bin/bash\n" > /app/cli/setconnectstring.sh \
	&& echo "sed -i 's/__GeoApiDbContext__: __.*__/__GeoApiDbContext__: __'__###ConnectionStrings__'__/'  /app/cli/appsettings.json" >> /app/cli/setconnectstring.sh \
	&& echo "sed -i 's/__GeoApiDbContext__: __.*__/__GeoApiDbContext__: __'__###ConnectionStrings__'__/'  /app/api/appsettings.json" >> /app/cli/setconnectstring.sh \
	&& echo "sed -i 's/__GeoApiDbContext__: __.*__/__GeoApiDbContext__: __'__###ConnectionStrings__'__/'  /src/src/Geo.Api/appsettings.json" >> /app/cli/setconnectstring.sh \
	&& echo "sed -i 's/__GeoApiDbContext__: __.*__/__GeoApiDbContext__: __'__###ConnectionStrings__'__/'  /src/src/Geo.DataSeeding/appsettings.json" >> /app/cli/setconnectstring.sh \
	&& sed -i 's/__/"/g' /app/cli/setconnectstring.sh \
	&& sed -i 's/###/\$/g' /app/cli/setconnectstring.sh \
	&& chmod ugo+x /app/cli/setconnectstring.sh

RUN echo "#!/bin/bash\n" > /app/cli/migrationrun.sh \
	&& echo 'eval dotnet-ef database update -s /src/src/Geo.Api --connection "$ConnectionStrings"' >> /app/cli/migrationrun.sh \
	&& chmod ugo+x /app/cli/migrationrun.sh

RUN echo "#!/bin/bash\n" > /app/cli/seeding.sh \
	&& echo "cd /app/cli/" >> /app/cli/seeding.sh \
	&& echo "/app/cli/Geo.DataSeeding" >> /app/cli/seeding.sh \
	&& chmod ugo+x /app/cli/seeding.sh


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
USER app
WORKDIR /app

COPY --from=build /app/api ./api
COPY --from=build /app/cli ./cli
COPY --from=build /root/.dotnet /home/app/.dotnet


ENV PATH="/home/app/.dotnet/tools:${PATH}"

ENTRYPOINT ["dotnet", "/app/api/Geo.Api.dll"]
