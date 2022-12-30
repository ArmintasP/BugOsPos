using BugOsPos.Application.Authentication.Common;
using BugOsPos.Application.Common.Interfaces.Authentication;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.Common.Errors;
using BugOsPos.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;

namespace BugOsPos.Application.Authentication.Commands.Login;

public sealed class CustomerLoginCommandHandler :
    IRequestHandler<CustomerLoginCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CustomerLoginCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        ICustomerRepository customerRepository,
        IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        CustomerLoginCommand request,
        CancellationToken cancellationToken)
    {
        if (await _customerRepository.GetCustomerByUsername(request.Username, request.FranchiseId) is not Customer customer)
            return Errors.Authentication.InvalidCredentials;

        if (!_passwordHasher.Verify(request.Password, customer.Password, customer.Salt))
            return Errors.Authentication.InvalidCredentials;

        var token = _jwtTokenGenerator.GenerateToken(customer);
        return new AuthenticationResult(customer, token);
    }
}
