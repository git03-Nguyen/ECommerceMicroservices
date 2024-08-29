FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 6200
EXPOSE 6201
ENV ASPNETCORE_HTTPS_PORTS=6200
ENV ASPNETCORE_HTTP_PORTS=6201

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Basket.Service/Basket.Service.csproj", "src/Basket.Service/"]
RUN dotnet restore "src/Basket.Service/Basket.Service.csproj"
COPY . .
WORKDIR "/src/src/Basket.Service"
RUN dotnet build "Basket.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Basket.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
RUN dotnet dev-certs https
RUN dotnet dev-certs https --trust

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Service.dll"]
