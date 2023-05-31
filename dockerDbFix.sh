#!/bin/bash
docker stop pg-docker
rm -r $HOME/docker/volumes/postgres
docker run --rm --name pg-docker -e POSTGRES_PASSWORD=docker -d -p 5432:5432 -v $HOME/docker/volumes/postgres:/var/lib/postgresql/data postgres
cd ./PearApi
sleep 10 # will fail without sleep
dotnet ef database update -c PearContext