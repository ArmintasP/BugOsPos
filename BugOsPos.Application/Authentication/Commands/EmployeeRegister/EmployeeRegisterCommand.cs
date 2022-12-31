using BugOsPos.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BugOsPos.Application.Authentication.Commands.EmployeeRegister;

public sealed record EmployeeRegisterCommand(
    int FranchiseId,
    int? GroupId,
    string Email,
    string Name,
    string Surname,
    string BankAccount,
    DateOnly DateOfBirth,
    string Address,
    string PhoneNumber,
    int ReadAccess,
    List<string> Roles,
    decimal Employment) : IRequest<ErrorOr<EmployeeRegisterResult>>;