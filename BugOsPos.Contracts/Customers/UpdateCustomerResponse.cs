using Microsoft.AspNetCore.Mvc;

namespace BugOsPos.Contracts.Customers;

public sealed record UpdateCustomerRequest(
    string Username,
    string Password,
    string Email,
    string Name,
    string Surname);
