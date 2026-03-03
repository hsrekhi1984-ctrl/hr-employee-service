using EmployeeService.Application.Abstractions;
using EmployeeService.Application.Employees.Commands;
using EmployeeService.Domain.Entities;
using FluentAssertions;

namespace EmployeeService.UnitTests;

public sealed class CreateEmployeeCommandHandlerTests
{
    [Fact]
    public async Task HandleAsync_Should_Throw_When_Email_Already_Exists()
    {
        var repository = new InMemoryEmployeeRepository(emailExists: true);
        var unitOfWork = new FakeUnitOfWork();
        var handler = new CreateEmployeeCommandHandler(repository, unitOfWork);

        var action = async () => await handler.HandleAsync(new CreateEmployeeCommand(
            "Jane", "Doe", "jane.doe@example.com", "Developer", new DateOnly(2024, 1, 1), 70000));

        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    private sealed class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly bool _emailExists;

        public InMemoryEmployeeRepository(bool emailExists)
        {
            _emailExists = emailExists;
        }

        public Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            Task.FromResult<Employee?>(null);

        public Task<IReadOnlyCollection<Employee>> GetAllAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyCollection<Employee>>(Array.Empty<Employee>());

        public Task AddAsync(Employee employee, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task DeleteAsync(Employee employee, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default) =>
            Task.FromResult(_emailExists);
    }

    private sealed class FakeUnitOfWork : IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(1);
    }
}
