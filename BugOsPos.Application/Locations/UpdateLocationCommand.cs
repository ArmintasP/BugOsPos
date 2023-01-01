using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.LocationAggregate;
using ErrorOr;
using FluentValidation;
using MediatR;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.LocationAggregate.ValueObjects;

namespace BugOsPos.Application.Locations;
public sealed record UpdateLocationCommand(
    int Id,
    string? Name,
    string? Adress) : IRequest<ErrorOr<UpdateLocationResult>>;
public sealed record UpdateLocationResult(Location Location);

public sealed class UpdateLocationValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationValidator() {}
}

public sealed class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, ErrorOr<UpdateLocationResult>>
{
    private readonly ILocationRepository _locationRepository;

    public UpdateLocationCommandHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<ErrorOr<UpdateLocationResult>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
          
  var location = await _locationRepository.GetLocationById(LocationId.New(request.Id));
        if (location is null)
            return Errors.Location.NotFound;

        location = Location.New(
            location.Id,
            request.Name ?? location.Name,
            request.Adress ?? location.Address);

        await _locationRepository.Update(location);

        return new UpdateLocationResult(location);
    }
}
