#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/OrganizationService/OrganizationService.API/OrganizationService.API.csproj", "Services/OrganizationService/OrganizationService.API/"]
COPY ["Services/OrganizationService/OrganizationService.Application/OrganizationService.Application.csproj", "Services/OrganizationService/OrganizationService.Application/"]
COPY ["Services/OrganizationService/OrganizationService.Domain/OrganizationService.Domain.csproj", "Services/OrganizationService/OrganizationService.Domain/"]
COPY ["Services/OrganizationService/OrganizationService.Infrastructure/OrganizationService.Infrastructure.csproj", "Services/OrganizationService/OrganizationService.Infrastructure/"]
RUN dotnet restore "Services/OrganizationService/OrganizationService.API/OrganizationService.API.csproj"
COPY . .
WORKDIR "/src/Services/OrganizationService/OrganizationService.API"
RUN dotnet build "OrganizationService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrganizationService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrganizationService.API.dll"]