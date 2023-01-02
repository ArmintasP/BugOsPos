using BugOsPos.Contracts.Common;
using BugOsPos.Contracts.Locations;
namespace BugOsPos.Contracts.Franchises;
public sealed record GetFranchiseByIdResponse(
    string Name,
    string PhoneNumber,
    string Email,
    List<Product> Products,
    List<Employee> Employees
    );

public sealed record Product(
    int Id,
    string Name,
    decimal Price,
    int FranchiseId);

public sealed record Employee(
    int Id,
    string EmployeeCode,
    string Password,
    int FranchiseId,
    string Email,
    string Name,
    string Surname,
    string BankAccount,
    DateOnly DateOfBirth,
    string Address,
    string PhoneNumber,
    decimal Rating,
    int ReadAccess,
    List<string> Roles,
    decimal Employment,
    List<ShiftSection>? Shifts);
