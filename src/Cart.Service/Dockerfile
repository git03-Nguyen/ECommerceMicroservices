FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 6400
EXPOSE 6401

ARG BUILD_ENV="Development"
ENV ASPNETCORE_ENVIRONMENT=$BUILD_ENV

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Cart.Service/Cart.Service.csproj", "src/Cart.Service/"]
RUN dotnet restore "src/Cart.Service/Cart.Service.csproj"
COPY . .
WORKDIR "/src/src/Cart.Service"
RUN dotnet build "Cart.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Cart.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.Service.dll"]
