#!/bin/bash

# Requirments: docker, dotnet ef, Linux, bash, Internet access
# To create new instance of database in dev env, you need to run this script on Linux with command:
#       source <path_to_script>

# Up postgres instance via docker
docker pull postgres:15-alpine3.21
docker run --name chandelier-store-db -e POSTGRES_USER="luver" -e POSTGRES_PASSWORD="987654321" -d -p 5432:5432  postgres:15-alpine3.21

# Db migrations to update db
cd ./microservices/microservices.CatalogAPI/
dotnet ef update
