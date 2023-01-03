using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.DiscountAggregate;
using BugOsPos.Domain.LoyaltyCardAggregate;
using BugOsPos.Domain.LoyaltyCardAggregate.ValueObjects;
using BugOsPos.Domain.LoyaltyDiscountAggregate;
using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugOsPos.Application.LoyaltyCards;

public sealed record CreateLoyaltyCardCommand(int CustomerId) : IRequest<ErrorOr<CreateLoyaltyCardResult>>;

public sealed record CreateLoyaltyCardResult(LoyaltyCard LoyaltyCard);

public sealed class CreateLoyaltyCardValidator : AbstractValidator<CreateLoyaltyCardCommand>
{
    public CreateLoyaltyCardValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}

public sealed class CreateLoyaltyCardQueryHandler : IRequestHandler<CreateLoyaltyCardCommand, ErrorOr<CreateLoyaltyCardResult>>
{
    private readonly ILoyaltyCardRepository _loyaltyCardRepository;

    public CreateLoyaltyCardQueryHandler(ILoyaltyCardRepository loyaltyCardRepository)
    {
        _loyaltyCardRepository = loyaltyCardRepository;
    }

    public async Task<ErrorOr<CreateLoyaltyCardResult>> Handle(CreateLoyaltyCardCommand request, CancellationToken cancellationToken)
    {
        LoyaltyCardId nextLoyaltyCardId = _loyaltyCardRepository.NextIdentity();
        LoyaltyCard loyaltyCard = LoyaltyCard.New(nextLoyaltyCardId, CustomerId.New(request.CustomerId), $"USER{nextLoyaltyCardId.Value}");
        _loyaltyCardRepository.Add(loyaltyCard);

        return new CreateLoyaltyCardResult(loyaltyCard);
    }
}
