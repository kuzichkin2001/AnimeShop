#!/bin/bash

docker-compose build
docker-compose up -d

docker build . -t anime_shop_webapi -f ./TestPet/Dockerfile
docker build . -t anime_shop_bot -f ./AnimeShopTelegramBot/Dockerfile

docker run anime_shop_webapi -p 5001:80 -p 443:443 --name webapi_container -d 
docker run anime_shop_bot --name tg_bot_container -d