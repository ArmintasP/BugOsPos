using BugOsPos.Application.Authentication.Common;
using BugOsPos.Application.Common.Interfaces.Authentication;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.Common.Errors;
using BugOsPos.Domain.CustomerAggregate;
using ErrorOr;
using MediatR;

namespace BugOsPos.Application.Authentication.Commands.CustomerRegister;

public sealed class CustomerRegisterCommandHandler :
    IRequestHandler<CustomerRegisterCommand, ErrorOr<CustomerAuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordProvider _passwordHasher;

    public CustomerRegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        ICustomerRepository customerRepository,
        IPasswordProvider passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<CustomerAuthenticationResult>> Handle(
        CustomerRegisterCommand request,
        CancellationToken cancellationToken)
    {
        var errors = new List<Error>();

        if (await _customerRepository.GetCustomerByUsername(request.Username, request.FranchiseId) is not null)
            errors.Add(Errors.Customer.DuplicateUsername);
        if (await _customerRepository.GetCustomerByEmail(request.Email, request.FranchiseId) is not null)
            errors.Add(Errors.Customer.DuplicateEmail);
        if (errors.Count > 0)
            return errors;

        var (hashedPassword, salt) = _passwordHasher.HashPassword(request.Password);

        var customer = Customer.New(
            _customerRepository.NextIdentity(),
            request.Username,
            hashedPassword,
            salt,
            request.Email,
            request.Name,
            request.Surname,
            null,
            request.FranchiseId);

        await _customerRepository.Add(customer);

        var token = _jwtTokenGenerator.GenerateToken(customer);
        return new CustomerAuthenticationResult(customer, token);
    }
}
