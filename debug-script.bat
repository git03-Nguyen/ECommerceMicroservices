@echo off
cd /d %~dp0

if "%1"=="" (
    echo Rebuilding all images...
    docker-compose -f docker-compose.yml -f docker-compose.override.yml down 
    docker-compose -f docker-compose.yml -f docker-compose.override.yml build 
    docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
) else (
    echo Rebuilding image for service: %1
    docker-compose -f docker-compose.yml -f docker-compose.override.yml down %1
    docker-compose -f docker-compose.yml -f docker-compose.override.yml build %1
    docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d %1
)