#!/bin/bash

read -p "Input migration name: " name
echo "Will create migration: $name"

dotnet ef migrations add $name --project ../Api.Counters.csproj --output-dir ./Database/Migrations
