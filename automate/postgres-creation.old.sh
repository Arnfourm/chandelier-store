#!/bin/bash

# Requirments: docker, dotnet ef, Linux, bash, Internet access
# To create new instance of database in dev env, you need to run this script on Linux with command:
#       source <path_to_script>

# Up postgres instance via docker
docker pull postgres:15-alpine3.21
docker run --name chandelier-store-db -e POSTGRES_USER="luver" -e POSTGRES_PASSWORD="987654321" -d -p 5432:5432  postgres:15-alpine3.21

# Db migrations to update db
# cd ./microservices/microservices.CatalogAPI/

# ! Приложение должно быть выключено
# ! Команды вводить в powershell (от администратора)
# ! Должен быть запущен Docker Desktop

# !!! Освободить порт 5432
# !!! Создать БД: psql -h localhost -U luver -p 5432 -d postgres, password: 987654321, create database <database_name>

# ! Добавить первую миграцию (иначе нечего обновлять)
# !!! Проверить, свежая ли версия миграции
# dotnet ef migrations add <название_миграции>

dotnet ef database update
