@echo off
docker-compose -f docker/docker-compose.yml down
TIMEOUT /T 10
docker-compose -f docker/docker-compose.yml build
TIMEOUT /T 10
docker-compose -f docker/docker-compose.yml up -d
TIMEOUT /T 10
docker exec webapi /app/buildcli/setconnectstring.sh
TIMEOUT /T 10
docker exec webapi /app/buildcli/migrationrun.sh 
TIMEOUT /T 10
docker exec webapi /app/buildcli/seeding.sh
TIMEOUT /T 10
start "" http://localhost:5080/swagger/index.html