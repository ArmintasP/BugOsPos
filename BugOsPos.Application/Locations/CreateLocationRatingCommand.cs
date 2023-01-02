using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.LocationAggregate;
using BugOsPos.Domain.LocationAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.Common.ValueObjects;

namespace BugOsPos.Application.Locations;

public sealed record CreateLocationRatingCommand(
    int Id,
    decimal RatingNumber) : IRequest<ErrorOr<CreateLocationRatingResult>>;
public sealed record CreateLocationRatingResult(Location Location);

public sealed class CreateLocationRatingValidator : AbstractValidator<CreateLocationRatingCommand>
{
    public CreateLocationRatingValidator() { }
}

public sealed class CreateLocationRatingCommandHandler : IRequestHandler<CreateLocationRatingCommand, ErrorOr<CreateLocationRatingResult>>
{
    private readonly ILocationRepository _locationRepository;

    public CreateLocationRatingCommandHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<ErrorOr<CreateLocationRatingResult>> Handle(CreateLocationRatingCommand request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetLocationById(LocationId.New(request.Id));
        if (location is null)
            return Errors.Location.NotFound;

        var updatedRating = (location.Rating.Value * location.Rating.NumberOfRatings + request.RatingNumber) / (location.Rating.NumberOfRatings + 1);
        var updatedCount = location.Rating.NumberOfRatings + 1;
        location = Location.New(
            location.Id,
            location.Name,
            location.Address,
            Rating.New(updatedRating, updatedCount));

        await _locationRepository.Update(location);

        return new CreateLocationRatingResult(location);
    }
}

