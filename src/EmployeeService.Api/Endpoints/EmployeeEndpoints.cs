using EmployeeService.Api.Authorization;
using EmployeeService.Api.Contracts;
using EmployeeService.Application.Abstractions;
using EmployeeService.Application.Employees.Commands;
using EmployeeService.Application.Employees.Queries;

namespace EmployeeService.Api.Endpoints;

public static class EmployeeEndpoints
{
    public static IEndpointRouteBuilder MapEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/employees").WithTags("Employees");

        group.MapGet("/", async (IEmployeeRepository repository, CancellationToken ct) =>
            {
                var handler = new GetEmployeesQueryHandler(repository);
                var result = await handler.HandleAsync(ct);
                return Results.Ok(result);
            })
            .RequireAuthorization("Employee.Read")
            .WithName("GetEmployees")
            .WithOpenApi();

        group.MapPost("/", async (
                CreateEmployeeRequest request,
                IEmployeeRepository repository,
                IUnitOfWork unitOfWork,
                CancellationToken ct) =>
            {
                var handler = new CreateEmployeeCommandHandler(repository, unitOfWork);
                var employee = await handler.HandleAsync(new CreateEmployeeCommand(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.JobTitle,
                    request.HireDate,
                    request.Salary), ct);

                return Results.Created($"/api/employees/{employee.Id}", employee);
            })
            .RequireAuthorization("Employee.Write")
            .WithName("CreateEmployee")
            .WithOpenApi();

        return app;
    }
}
