using EmployeeService.Application.Abstractions;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Employees.Commands;

public sealed record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    DateOnly HireDate,
    decimal Salary);

public sealed class CreateEmployeeCommandHandler
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<EmployeeDto> HandleAsync(CreateEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        var exists = await _employeeRepository.EmailExistsAsync(command.Email, cancellationToken);
        if (exists) throw new InvalidOperationException("An employee with this email already exists.");

        var employee = new Employee(
            command.FirstName,
            command.LastName,
            command.Email,
            command.JobTitle,
            command.HireDate,
            command.Salary);

        await _employeeRepository.AddAsync(employee, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return EmployeeDto.From(employee);
    }
}
