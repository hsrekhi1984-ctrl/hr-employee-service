using EmployeeService.Domain.Enums;

namespace EmployeeService.Domain.Entities;

public sealed class Employee
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string JobTitle { get; private set; } = string.Empty;
    public DateOnly HireDate { get; private set; }
    public decimal Salary { get; private set; }
    public EmploymentStatus Status { get; private set; } = EmploymentStatus.Active;

    private Employee() { }

    public Employee(
        string firstName,
        string lastName,
        string email,
        string jobTitle,
        DateOnly hireDate,
        decimal salary)
    {
        UpdatePersonalInfo(firstName, lastName, email);
        UpdateJobInfo(jobTitle, salary);
        HireDate = hireDate;
    }

    public void UpdatePersonalInfo(string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required.");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required.");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim().ToLowerInvariant();
    }

    public void UpdateJobInfo(string jobTitle, decimal salary)
    {
        if (string.IsNullOrWhiteSpace(jobTitle)) throw new ArgumentException("Job title is required.");
        if (salary < 0) throw new ArgumentOutOfRangeException(nameof(salary), "Salary must be non-negative.");

        JobTitle = jobTitle.Trim();
        Salary = salary;
    }

    public void ChangeStatus(EmploymentStatus status) => Status = status;
}
