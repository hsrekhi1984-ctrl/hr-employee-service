# HR Employee Service (.NET 8)

Production-ready Employee Web API built with Clean Architecture and the following capabilities:

- .NET 8 Web API (Minimal APIs)
- Clean architecture layers (Domain, Application, Infrastructure, API)
- PostgreSQL + EF Core
- OpenTelemetry traces/metrics/logs export via OTLP
- Health checks (`/health`)
- Serilog structured logging
- RBAC authorization policies
- Docker multi-stage build
- Unit tests with xUnit + FluentAssertions

## Project structure

- `src/EmployeeService.Domain`: Domain entities and enums
- `src/EmployeeService.Application`: Application use cases and abstractions
- `src/EmployeeService.Infrastructure`: EF Core context + repository implementations
- `src/EmployeeService.Api`: API endpoints, DI, auth, telemetry, health checks
- `tests/EmployeeService.UnitTests`: Unit tests

## RBAC policies

- `Employee.Read`: `Admin`, `HrManager`, `EmployeeReader`
- `Employee.Write`: `Admin`, `HrManager`

## Run with Docker

```bash
docker build -t employee-service -f Dockerfile .
docker run --rm -p 8080:8080 employee-service
```

## Local development

```bash
dotnet restore
dotnet build
dotnet test
```

## Environment variables

- `ConnectionStrings__PostgreSql`
- `OTEL_EXPORTER_OTLP_ENDPOINT`
- `ASPNETCORE_ENVIRONMENT`
