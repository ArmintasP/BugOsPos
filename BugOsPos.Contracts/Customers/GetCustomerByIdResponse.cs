namespace BugOsPos.Contracts.Customers;

public sealed record GetCustomerByIdResponse(
    int Id,
    int FranchiseId,
    string Username,
    string Email,
    string Name,
    string Surname,
    string Address,
    bool IsBlocked);