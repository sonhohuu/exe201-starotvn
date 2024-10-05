#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Exe.Starot.Api/Exe.Starot.Api.csproj", "Exe.Starot.Api/"]
COPY ["Exe.Starot.Application/Exe.Starot.Application.csproj", "Exe.Starot.Application/"]
COPY ["Exe.Starot.Domain/Exe.Starot.Domain.csproj", "Exe.Starot.Domain/"]
COPY ["Exe.Starot.Infrastructure/Exe.Starot.Infrastructure.csproj", "Exe.Starot.Infrastructure/"]
RUN dotnet restore "./Exe.Starot.Api/Exe.Starot.Api.csproj"
COPY . .
WORKDIR "/src/Exe.Starot.Api"
RUN dotnet build "./Exe.Starot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Exe.Starot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Exe.Starot.Api.dll"]