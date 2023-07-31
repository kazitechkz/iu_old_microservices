#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/AcademicYearService/AcademicYearService.API/AcademicYearService.API.csproj", "Services/AcademicYearService/AcademicYearService.API/"]
COPY ["Services/AcademicYearService/AcademicYearService.Application/AcademicYearService.Application.csproj", "Services/AcademicYearService/AcademicYearService.Application/"]
COPY ["Services/AcademicYearService/AcademicYearService.Domain/AcademicYearService.Domain.csproj", "Services/AcademicYearService/AcademicYearService.Domain/"]
COPY ["Services/AcademicYearService/AcademicYearService.Infrastructure/AcademicYearService.Infrastructure.csproj", "Services/AcademicYearService/AcademicYearService.Infrastructure/"]
RUN dotnet restore "Services/AcademicYearService/AcademicYearService.API/AcademicYearService.API.csproj"
COPY . .
WORKDIR "/src/Services/AcademicYearService/AcademicYearService.API"
RUN dotnet build "AcademicYearService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AcademicYearService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcademicYearService.API.dll"]