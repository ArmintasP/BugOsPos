using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.LocationAggregate;
using BugOsPos.Domain.LocationAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;
using BugOsPos.Domain.Common.ErrorsCollection;

namespace BugOsPos.Application.Locations;

public sealed record GetLocationByIdQuery(int Id) : IRequest<ErrorOr<GetLocationByIdResult>>;

public sealed record GetLocationByIdResult(Location Location);

public sealed class GetLocationByIdValidator : AbstractValidator<GetLocationByIdQuery>
{
    public GetLocationByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, ErrorOr<GetLocationByIdResult>>
{
    private readonly ILocationRepository _locationRepository;

    public GetLocationByIdQueryHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<ErrorOr<GetLocationByIdResult>> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _locationRepository.GetLocationById(LocationId.New(request.Id));
        if (customer is null)
            return Errors.Location.NotFound;

        return new GetLocationByIdResult(customer);
    }
}
