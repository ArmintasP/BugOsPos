using BugOsPos.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BugOsPos.Application.Authentication.Commands.Login;

public sealed record CustomerLoginCommand(
    int FranchiseId,
    string Username,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
