#!/bin/bash

# Requirments: docker, dotnet ef, Linux, bash, Internet access
# Instructions:
# Run this file from DIRECTORY automate with Linux OS by command
#   source ./run-entire-product.sh

cd ../
docker compose -f ./docker-compose.yml --env-file=./.env up -d

cd ./microservices/microservice.SupplyAPI
dotnet ef database update --connection "User ID=luver;Password=987654321;Host=127.0.0.1;Port=5432;Database=chandelier_store_supply;"

cd ../microservices.CatalogAPI
dotnet ef database update --connection "User ID=luver;Password=987654321;Host=127.0.0.1;Port=5432;Database=chandelier_store_catalog;"

cd ../microservices.UserAPI
dotnet ef database update --connection "User ID=luver;Password=987654321;Host=127.0.0.1;Port=5432;Database=chandelier_store_user;"