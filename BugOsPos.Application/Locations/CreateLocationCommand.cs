using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.LocationAggregate;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BugOsPos.Application.Locations;

public sealed record CreateLocationCommand(
    string Name,
    string Adress) : IRequest<ErrorOr<CreateLocationResult>>;
public sealed record CreateLocationResult(Location Location);

public sealed class CreateLocationValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Adress).NotEmpty();
    }
}

public sealed class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, ErrorOr<CreateLocationResult>>
{
    private readonly ILocationRepository _locationRepository;

    public CreateLocationCommandHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<ErrorOr<CreateLocationResult>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = Location.New(
            _locationRepository.NextIdentity(),
            request.Name,
            request.Adress);

        await _locationRepository.Add(location);

        return new CreateLocationResult(location);
    }
}

