FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 6400
EXPOSE 6401
ENV ASPNETCORE_HTTPS_PORTS=6400
ENV ASPNETCORE_HTTP_PORTS=6401

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Order.Service/Order.Service.csproj", "src/Order.Service/"]
RUN dotnet restore "src/Order.Service/Order.Service.csproj"
COPY . .
WORKDIR "/src/src/Order.Service"
RUN dotnet build "Order.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Order.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Service.dll"]
