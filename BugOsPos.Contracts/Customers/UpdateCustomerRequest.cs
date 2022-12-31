using Microsoft.AspNetCore.Mvc;

namespace BugOsPos.Contracts.Customers;

public sealed record UpdateCustomerResponse(
    int Id,
    string Username,
    string Email,
    string Name,
    string Surname);
