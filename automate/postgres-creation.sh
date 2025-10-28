#!/bin/bash

# Requirments: docker, Linux, bash, Internet access
# To create new instance of database in dev env, you need to run this script on Linux with command:
#       source <path_to_script>

# docker images
# docker pull postgres:15-alpine3.21
docker run --name chandelier-store-db -e POSTGRES_USER="luver" -e POSTGRES_PASSWORD="987654321" -d -p 5432:5432  postgres:15-alpine3.21
