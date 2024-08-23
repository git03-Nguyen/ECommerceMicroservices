FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 6300
EXPOSE 6301

ARG BUILD_ENV="Development"
ENV ASPNETCORE_ENVIRONMENT=$BUILD_ENV

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Product.Service/Product.Service.csproj", "src/Product.Service/"]
RUN dotnet restore "src/Product.Service/Product.Service.csproj"
COPY . .
WORKDIR "/src/src/Product.Service"
RUN dotnet build "Product.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Product.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.Service.dll"]
