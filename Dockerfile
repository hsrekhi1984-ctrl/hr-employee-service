# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Directory.Build.props ./
COPY EmployeeService.sln ./
COPY src/EmployeeService.Domain/EmployeeService.Domain.csproj src/EmployeeService.Domain/
COPY src/EmployeeService.Application/EmployeeService.Application.csproj src/EmployeeService.Application/
COPY src/EmployeeService.Infrastructure/EmployeeService.Infrastructure.csproj src/EmployeeService.Infrastructure/
COPY src/EmployeeService.Api/EmployeeService.Api.csproj src/EmployeeService.Api/
COPY tests/EmployeeService.UnitTests/EmployeeService.UnitTests.csproj tests/EmployeeService.UnitTests/

RUN dotnet restore EmployeeService.sln

COPY . .
RUN dotnet publish src/EmployeeService.Api/EmployeeService.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EmployeeService.Api.dll"]
