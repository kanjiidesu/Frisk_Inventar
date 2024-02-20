#! /bin/bash

cd FriskInventarWebAPP

npm install
npm run build

cd ..

#Start docker containers
docker compose build
docker compose up -d