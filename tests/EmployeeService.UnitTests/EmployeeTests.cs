using EmployeeService.Domain.Entities;
using EmployeeService.Domain.Enums;
using FluentAssertions;

namespace EmployeeService.UnitTests;

public sealed class EmployeeTests
{
    [Fact]
    public void Constructor_Should_Create_Employee_With_Normalized_Email()
    {
        var employee = new Employee(
            "Jane",
            "Doe",
            "JANE.DOE@EXAMPLE.COM",
            "Software Engineer",
            new DateOnly(2024, 1, 10),
            100000);

        employee.Email.Should().Be("jane.doe@example.com");
        employee.Status.Should().Be(EmploymentStatus.Active);
    }

    [Fact]
    public void UpdateJobInfo_Should_Throw_When_Salary_Is_Negative()
    {
        var employee = new Employee(
            "John",
            "Smith",
            "john.smith@example.com",
            "HR Specialist",
            new DateOnly(2023, 2, 1),
            60000);

        var action = () => employee.UpdateJobInfo("HR Lead", -1);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}
