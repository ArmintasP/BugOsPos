using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.LoyaltyCardAggregate;
using BugOsPos.Domain.Common.ErrorsCollection;
using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugOsPos.Domain.DiscountAggregate;
using BugOsPos.Domain.LoyaltyDiscountAggregate;

namespace BugOsPos.Application.LoyaltyCards;

public sealed record GetLoyaltyCardByIdQuery(int Id) : IRequest<ErrorOr<GetLoyaltyCardByIdResult>>;

public sealed record GetLoyaltyCardByIdResult(LoyaltyCard LoyaltyCard, List<Discount> Discounts);

public sealed class GetLoyaltyCardByIdValidator : AbstractValidator<GetLoyaltyCardByIdQuery>
{
    public GetLoyaltyCardByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class GetLoyaltyCardByIdQueryHandler : IRequestHandler<GetLoyaltyCardByIdQuery, ErrorOr<GetLoyaltyCardByIdResult>>
{
    private readonly ILoyaltyCardRepository _loyaltyCardRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly ILoyaltyDiscountRepository _loyaltyDiscountRepository;

    public GetLoyaltyCardByIdQueryHandler(ILoyaltyCardRepository loyaltyCardRepository, IDiscountRepository discountRepository, ILoyaltyDiscountRepository loyaltyDiscountRepository)
    {
        _loyaltyCardRepository = loyaltyCardRepository;
        _discountRepository = discountRepository;
        _loyaltyDiscountRepository = loyaltyDiscountRepository;
    }

    public async Task<ErrorOr<GetLoyaltyCardByIdResult>> Handle(GetLoyaltyCardByIdQuery request, CancellationToken cancellationToken)
    {
        LoyaltyCard? loyaltyCard = await _loyaltyCardRepository.GetLoyaltyCardById(request.Id);
        if (loyaltyCard is null) 
        {
            return Errors.LoyaltyCard.NotFound;
        }

        List<LoyaltyDiscount>? loyaltyDiscounts = _loyaltyDiscountRepository.GetLoyaltyDiscountsByLoyaltyCardId(loyaltyCard.Id.Value);

        if(loyaltyDiscounts is null)
        {
            return new GetLoyaltyCardByIdResult(loyaltyCard, new List<Discount>());
        }

        List<Discount> discounts = new List<Discount>();
        foreach(LoyaltyDiscount loyaltyDiscount in loyaltyDiscounts)
        {
            Discount? discount = await _discountRepository.GetDiscountById(loyaltyDiscount.DiscountId.Value);
            if (discount is not null)
            {
                discounts.Add(discount);
            }
        }

        return new GetLoyaltyCardByIdResult(loyaltyCard, discounts);
    }
}
