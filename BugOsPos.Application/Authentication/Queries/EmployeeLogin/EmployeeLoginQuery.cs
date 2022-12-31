using BugOsPos.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BugOsPos.Application.Authentication.Queries.EmployeeLogin;

public sealed record EmployeeLoginQuery(
    int FranchiseId,
    string EmployeeCode,
    string Password) : IRequest<ErrorOr<EmployeeLoginResult>>;
