using BugOsPos.Application.Authentication.Queries.CustomerLogin;
using BugOsPos.Application.Common.Behaviors;
using BugOsPos.Application.Common.Interfaces.Authentication;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.CustomerAggregate;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Customers;

public sealed record UpdateCustomerCommand(
    int Id,
    string Username,
    string Password,
    string Email,
    string Name,
    string Surname) : IRequest<ErrorOr<UpdateCustomerResult>>;

public sealed record UpdateCustomerResult(Customer Customer);

public sealed class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    private const int MinUsernameLength = 4;
    private const int MaxUsernameLength = 12;
    private const int MinPasswordLength = 8;
    private const int MaxPasswordLength = 32;
    
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.Username)
            .MinimumLength(MinUsernameLength)
            .MaximumLength(MaxUsernameLength);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).Password(MinPasswordLength, MaxPasswordLength);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
    }
}

    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ErrorOr<UpdateCustomerResult>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordProvider _passwordProvider;

    public UpdateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IPasswordProvider passwordProvider)
    {
        _customerRepository = customerRepository;
        _passwordProvider = passwordProvider;
    }

    public async Task<ErrorOr<UpdateCustomerResult>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<Error>();
        var customer = await _customerRepository.GetCustomerById(request.Id);
        
        if (customer is null)
            return Errors.Customer.NotFound;
        
        if (request.Username != customer.Username && 
            await _customerRepository.GetCustomerByUsername(request.Username, customer.FranchiseId.Value) is not null)
        {
            errors.Add(Errors.Customer.DuplicateUsername);
        }
        if (request.Email != customer.Email && 
            await _customerRepository.GetCustomerByEmail(request.Email, customer.FranchiseId.Value) is not null)
        {
            errors.Add(Errors.Customer.DuplicateEmail);
        }
        
        if (errors.Count > 0)
            return errors;

        var (hashedPassword, salt) = _passwordProvider.HashPassword(request.Password);
        
        customer = Customer.New(
            customer.Id,
            request.Username,
            hashedPassword,
            salt,
            request.Email,
            request.Name,
            request.Surname,
            null,
            customer.FranchiseId.Value);

        await _customerRepository.Update(customer);
        return new UpdateCustomerResult(customer);
    }
}
