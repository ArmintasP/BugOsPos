using BugOsPos.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BugOsPos.Application.Authentication.Commands.Register;

public sealed record CustomerRegisterCommand(
    int FranchiseId,
    string Username,
    string Email,
    string Password,
    string Name,
    string Surname) : IRequest<ErrorOr<AuthenticationResult>>;
