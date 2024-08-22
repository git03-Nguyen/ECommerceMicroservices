@echo off
cd /d %~dp0

if "%1"=="" (
    echo Rebuilding all images...
    docker-compose down
    docker-compose build
    docker-compose up -d
) else (
    echo Rebuilding image for service: %1
    docker-compose build %1
    docker-compose up -d %1
)