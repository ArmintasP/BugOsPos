using BugOsPos.Application.Common.Interfaces.Authentication;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.EmployeeAggregate;
using ErrorOr;
using MediatR;

namespace BugOsPos.Application.Authentication.Queries.EmployeeLogin;

public sealed class EmployeeLoginQueryHandler :
    IRequestHandler<EmployeeLoginQuery, ErrorOr<EmployeeLoginResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordProvider _passwordHasher;

    public EmployeeLoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IEmployeeRepository employeeRepository,
        IPasswordProvider passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _employeeRepository = employeeRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<EmployeeLoginResult>> Handle(
        EmployeeLoginQuery request,
        CancellationToken cancellationToken)
    {
        if (await _employeeRepository.GetEmployeeByEmployeeCode(request.EmployeeCode, request.FranchiseId) is not Employee employee)
            return Errors.Authentication.InvalidCredentials;

        if (!_passwordHasher.VerifyPassword(request.Password, employee.Password, employee.Salt))
            return Errors.Authentication.InvalidCredentials;

        var token = _jwtTokenGenerator.GenerateToken(employee);
        return new EmployeeLoginResult(token);
    }
}
