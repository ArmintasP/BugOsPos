using BugOsPos.Application.Authentication.Queries.CustomerLogin;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.CustomerAggregate;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Customers;

public sealed record GetCustomerByIdQuery(int Id) : IRequest<ErrorOr<GetCustomerByIdResult>>;

public sealed record GetCustomerByIdResult(Customer Customer);

public sealed class GetCustomerByIdValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

    public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ErrorOr<GetCustomerByIdResult>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<GetCustomerByIdResult>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerById(request.Id);
        if (customer is null)
            return Errors.Customer.NotFound;
        
        return new GetCustomerByIdResult(customer);
    }
}
