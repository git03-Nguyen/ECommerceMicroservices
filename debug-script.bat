@echo off

REM This script is used to rebuild the images for the services in the docker-compose file.
REM Usage: debug-script.bat [services] [--help] [--list] [--databases]
REM services: The name of the services to rebuild. If not provided, all services will be rebuilt.
REM --all: Rebuild all services.
REM --rabbitmq: Rebuild RabbitMQ.
REM --db: Rebuild all databases.
REM --support: Rebuild all support services.
REM --list: List all services in the docker-compose file.

if "%1"=="" (
    echo Usage: debug-script.bat [services] [--all] [--db] [--rabbitmq] [--support] [--list]
    echo.
    echo services: The name of the services to rebuild. If not provided, all services will be rebuilt.
    echo --all: Rebuild all services.
    echo --db: Rebuild all databases.
    echo --rabbitmq: Rebuild RabbitMQ.
    echo --support: Rebuild all support services.
    echo --list: List all services in the docker-compose file.
    
) else if "%1"=="--list" (
    echo Listing services...
    docker-compose -f docker-compose.yml -f docker-compose.override.yml config --services
    
) else if "%1"=="--db" (
    echo Rebuilding databases...
    docker-compose -f docker-compose.yml -f docker-compose.override.yml down authdb basketdb catalogdb orderdb
    docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d authdb basketdb catalogdb orderdb
    
) else if "%1"=="--rabbitmq" (
    echo Rebuilding RabbitMQ...
    docker-compose -f docker-compose.yml -f docker-compose.override.yml down rabbitmq
    docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d rabbitmq
    
) else if "%1"=="--support" (
    echo Rebuilding support services...
    docker-compose -f docker-compose.yml -f docker-compose.override.yml down authdb basketdb catalogdb orderdb rabbitmq
    docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d authdb basketdb catalogdb orderdb rabbitmq
    
) else if "%1"=="--all" (
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