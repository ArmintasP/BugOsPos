using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.DiscountAggregate;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;
using BugOsPos.Domain.LoyaltyCardAggregate;
using BugOsPos.Domain.LoyaltyCardAggregate.ValueObjects;
using BugOsPos.Domain.LoyaltyDiscountAggregate;
using BugOsPos.Domain.ProductAggregate.ValueObjects;
using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BugOsPos.Application.LoyaltyCards;
public sealed record AddLoyaltyCardDiscountsCommand(int LoyaltyCardId, IEnumerable<int> DiscountIds) : IRequest<ErrorOr<AddLoyaltyCardDiscountsResult>>;

public sealed record AddLoyaltyCardDiscountsResult(LoyaltyCard LoyaltyCard, List<Discount> Discounts);

public sealed class AddLoyaltyCardDiscountsValidator : AbstractValidator<AddLoyaltyCardDiscountsCommand>
{
    public AddLoyaltyCardDiscountsValidator()
    {
        RuleFor(x => x.LoyaltyCardId).NotEmpty();
        RuleFor(x => x.DiscountIds).NotEmpty();
    }
}

public sealed class AddLoyaltyCardDiscountsCommandHandler : IRequestHandler<AddLoyaltyCardDiscountsCommand, ErrorOr<AddLoyaltyCardDiscountsResult>>
{
    private readonly ILoyaltyCardRepository _loyaltyCardRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly ILoyaltyDiscountRepository _loyaltyDiscountRepository;

    public AddLoyaltyCardDiscountsCommandHandler(
        ILoyaltyCardRepository loyaltyCardRepository, 
        IDiscountRepository discountRepository, 
        ILoyaltyDiscountRepository loyaltyDiscountRepository)
    {
        _loyaltyCardRepository = loyaltyCardRepository;
        _discountRepository = discountRepository;
        _loyaltyDiscountRepository = loyaltyDiscountRepository;
    }

    public async Task<ErrorOr<AddLoyaltyCardDiscountsResult>> Handle(AddLoyaltyCardDiscountsCommand request, CancellationToken cancellationToken)
    {
        LoyaltyCard? loyaltyCard = await _loyaltyCardRepository.GetLoyaltyCardById(request.LoyaltyCardId);
        if (loyaltyCard is null)
        {
            return Domain.Common.ErrorsCollection.Errors.LoyaltyCard.NotFound;
        }
        List<LoyaltyDiscount>? loyaltyDiscounts = _loyaltyDiscountRepository.GetLoyaltyDiscountsByLoyaltyCardId(loyaltyCard.Id.Value);
        foreach (int discountId in request.DiscountIds)
        {
            Discount? discount = await _discountRepository.GetDiscountById(DiscountId.New(discountId));
            if (discount is null)
            {
                return Domain.Common.ErrorsCollection.Errors.Discount.NotFound;
            }
            if (loyaltyDiscounts != null && loyaltyDiscounts.Any(x => x.DiscountId.Value == discount.Id.Value))
            {
                continue;
            }
            LoyaltyDiscount loyaltyDiscount = LoyaltyDiscount.New(_loyaltyDiscountRepository.NextIdentity(), LoyaltyCardId.New(request.LoyaltyCardId), ProductId.New(1), DiscountId.New(discountId));
            await _loyaltyDiscountRepository.Add(loyaltyDiscount);
        }

        loyaltyDiscounts = _loyaltyDiscountRepository.GetLoyaltyDiscountsByLoyaltyCardId(loyaltyCard.Id.Value);
   
        List<Discount> discounts = new();
        
        if (loyaltyDiscounts == null)
        {
            return new AddLoyaltyCardDiscountsResult(loyaltyCard, discounts);
        }
        
        foreach (LoyaltyDiscount loyaltyDiscount in loyaltyDiscounts)
        {
            Discount? discount = await _discountRepository.GetDiscountById(loyaltyDiscount.DiscountId);
            if (discount is not null)
            {
                discounts.Add(discount);
            }
        }

        return new AddLoyaltyCardDiscountsResult(loyaltyCard, discounts);
    }
}
