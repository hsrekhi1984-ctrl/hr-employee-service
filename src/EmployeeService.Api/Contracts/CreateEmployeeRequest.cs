namespace EmployeeService.Api.Contracts;

public sealed record CreateEmployeeRequest(
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    DateOnly HireDate,
    decimal Salary);
