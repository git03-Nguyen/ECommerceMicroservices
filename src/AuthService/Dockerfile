FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 6100
ENV ASPNETCORE_URLS=http://+:6100

ARG BUILD_ENV="Development"
ENV ASPNETCORE_ENVIRONMENT=$BUILD_ENV

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AuthService/AuthService.csproj", "src/AuthService/"]
RUN dotnet restore "src/AuthService/AuthService.csproj"
COPY . .
WORKDIR "/src/src/AuthService"
RUN dotnet build "AuthService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AuthService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.dll"]
