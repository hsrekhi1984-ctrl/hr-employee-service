using EmployeeService.Application.Abstractions;

namespace EmployeeService.Application.Employees.Queries;

public sealed class GetEmployeesQueryHandler
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IReadOnlyCollection<EmployeeDto>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var employees = await _employeeRepository.GetAllAsync(cancellationToken);
        return employees.Select(EmployeeDto.From).ToArray();
    }
}
