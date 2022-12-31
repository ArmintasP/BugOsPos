using BugOsPos.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BugOsPos.Application.Authentication.Queries.CustomerLogin;

public sealed record CustomerLoginQuery(
    int FranchiseId,
    string Username,
    string Password) : IRequest<ErrorOr<CustomerAuthenticationResult>>;
