using BugOsPos.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BugOsPos.Application.Authentication.Commands.CustomerRegister;

public sealed record CustomerRegisterCommand(
    int FranchiseId,
    string Username,
    string Email,
    string Password,
    string Name,
    string Surname) : IRequest<ErrorOr<CustomerAuthenticationResult>>;
