#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ASPNETCoreMVCTraining/ASPNETCoreMVCTraining.csproj", "ASPNETCoreMVCTraining/"]
RUN dotnet restore "ASPNETCoreMVCTraining/ASPNETCoreMVCTraining.csproj"
COPY . .
WORKDIR "/src/ASPNETCoreMVCTraining"
RUN dotnet build "./ASPNETCoreMVCTraining.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ASPNETCoreMVCTraining.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASPNETCoreMVCTraining.dll"]