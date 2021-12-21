  
#!bin/bash
docker-compose build counters
docker-compose rm --stop --force counters
docker-compose up -d
read -n 1 -s -r -p "Press any key to continue"