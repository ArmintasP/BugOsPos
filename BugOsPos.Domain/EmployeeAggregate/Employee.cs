using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.Common.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate.ValueObjects;

namespace BugOsPos.Domain.EmployeeAggregate;

public sealed class Employee : AggregateRoot<EmployeeId>
{
    public string EmployeeCode { get; }
    public string Password { get; }
    public FranchiseId FranchiseId { get; }
    public GroupId GroupId { get; }
    public int ReadAccess { get; }
    public string Email { get; }
    public string Name { get; }
    public string Surname { get; }
    public string PhoneNumber { get; }
    public string Address { get; }
    public string BankAccount { get; }
    public decimal Employment { get; }
    public Rating Rating { get; }
    public List<EmployeeType> Type { get; }
    public DateOnly DateOfBirth { get; }

    private Employee(
    EmployeeId id,
    string employeeCode,
    string password,
    FranchiseId franchiseId,
    GroupId groupId,
    int readAccess,
    string email,
    string name,
    string surname,
    string phoneNumber,
    string address,
    string bankAccount,
    decimal employment,
    List<EmployeeType> type,
    DateOnly dateOfBirth)
    : base(id)
    {
        EmployeeCode = employeeCode;
        Password = password;
        FranchiseId = franchiseId;
        GroupId = groupId;
        ReadAccess = readAccess;
        Email = email;
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        Address = address;
        BankAccount = bankAccount;
        Employment = employment;
        Rating = Rating.New();
        Type = type;
        DateOfBirth = dateOfBirth;
    }

    public static Employee New(
    EmployeeId id,
    string employeeCode,
    string password,
    FranchiseId franchiseId,
    GroupId groupId,
    int readAccess,
    string email,
    string name,
    string surname,
    string phoneNumber,
    string address,
    string bankAccount,
    decimal employment,
    List<EmployeeType> type,
    DateOnly dateOfBirth)
    {
        return new Employee(
            id,
            employeeCode,
            password,
            franchiseId,
            groupId,
            readAccess,
            email,
            name,
            surname,
            phoneNumber,
            address,
            bankAccount,
            employment,
            type,
            dateOfBirth);
    }
}

public enum EmployeeType
{
    Manager = 0,
    GroupManager = 1,
    Cashier = 2,
    Courier = 3,
    Specialist = 4
}
