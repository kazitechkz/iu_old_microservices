#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/SubjectService/SubjectService.API/SubjectService.API.csproj", "Services/SubjectService/SubjectService.API/"]
COPY ["Services/SubjectService/SubjectService.Application/SubjectService.Application.csproj", "Services/SubjectService/SubjectService.Application/"]
COPY ["Services/SubjectService/SubjectService.Domain/SubjectService.Domain.csproj", "Services/SubjectService/SubjectService.Domain/"]
COPY ["Services/SubjectService/SubjectService.Infrastructure/SubjectService.Infrastructure.csproj", "Services/SubjectService/SubjectService.Infrastructure/"]
RUN dotnet restore "Services/SubjectService/SubjectService.API/SubjectService.API.csproj"
COPY . .
WORKDIR "/src/Services/SubjectService/SubjectService.API"
RUN dotnet build "SubjectService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SubjectService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SubjectService.API.dll"]