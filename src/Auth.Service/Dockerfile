FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 6100
EXPOSE 6101
ENV ASPNETCORE_HTTPS_PORTS=6100
ENV ASPNETCORE_HTTP_PORTS=6101

ARG BUILD_ENV="Development"
ENV ASPNETCORE_ENVIRONMENT=$BUILD_ENV

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Auth.Service/Auth.Service.csproj", "src/Auth.Service/"]
RUN dotnet restore "src/Auth.Service/Auth.Service.csproj"
COPY . .
WORKDIR "/src/src/Auth.Service"
RUN dotnet build "Auth.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Auth.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Service.dll"]
