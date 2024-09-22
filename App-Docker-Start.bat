@echo off
docker-compose -f docker/docker-compose.yml up -d
docker exec webapi /app/buildcli/setconnectstring.sh