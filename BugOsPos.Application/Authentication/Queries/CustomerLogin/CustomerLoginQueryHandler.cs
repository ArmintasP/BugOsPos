using BugOsPos.Application.Authentication.Common;
using BugOsPos.Application.Common.Interfaces.Authentication;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;

namespace BugOsPos.Application.Authentication.Queries.CustomerLogin;

public sealed class CustomerLoginQueryHandler :
    IRequestHandler<CustomerLoginQuery, ErrorOr<CustomerAuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordProvider _passwordHasher;

    public CustomerLoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        ICustomerRepository customerRepository,
        IPasswordProvider passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<CustomerAuthenticationResult>> Handle(
        CustomerLoginQuery request,
        CancellationToken cancellationToken)
    {
        if (await _customerRepository.GetCustomerByUsername(request.Username, request.FranchiseId) is not Customer customer)
            return Errors.Authentication.InvalidCredentials;

        if (!_passwordHasher.VerifyPassword(request.Password, customer.Password, customer.Salt))
            return Errors.Authentication.InvalidCredentials;

        var token = _jwtTokenGenerator.GenerateToken(customer);
        return new CustomerAuthenticationResult(customer, token);
    }
}
