#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Inventory/Api/Api.csproj", "src/Services/Inventory/Api/"]
COPY ["src/Services/Inventory/Application/Application.csproj", "src/Services/Inventory/Application/"]
COPY ["src/Services/Inventory/Infrastructure/Infrastructure.csproj", "src/Services/Infrastructure/Infrastructure/"]
RUN dotnet restore "./src/Services/Inventory/Api/Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Inventory/Api"
RUN dotnet build "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory.Api.dll"]