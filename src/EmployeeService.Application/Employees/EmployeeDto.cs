using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Enums;

namespace EmployeeService.Application.Employees;

public sealed record EmployeeDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    DateOnly HireDate,
    decimal Salary,
    EmploymentStatus Status)
{
    public static EmployeeDto From(Employee employee) =>
        new(employee.Id, employee.FirstName, employee.LastName, employee.Email, employee.JobTitle, employee.HireDate, employee.Salary, employee.Status);
}
