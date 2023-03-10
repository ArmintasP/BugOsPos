using BugOsPos.Application.Authentication.Commands.CustomerRegister;
using BugOsPos.Application.Authentication.Common;
using BugOsPos.Application.Authentication.Queries.CustomerLogin;
using BugOsPos.Application.Customers;
using BugOsPos.Application.LoyaltyCards;
using BugOsPos.Contracts.CustomerAuthentication;
using BugOsPos.Contracts.Customers;
using BugOsPos.Contracts.LoyaltyCards;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using Mapster;
using System.Linq;

namespace BugOsPos.Api.Mappings;

public sealed class LoyaltyCardMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetLoyaltyCardByIdRequest, GetLoyaltyCardByIdQuery>();
        config.NewConfig<GetLoyaltyCardByIdResult, GetLoyaltyCardByIdResponse>()
            .Map(dest => dest.LoyaltyCardId, src => src.LoyaltyCard.Id.Value)
            .Map(dest => dest.CustomerId, src => src.LoyaltyCard.CustomerId.Value)
            .Map(dest => dest.LoyaltyCardCode, src => src.LoyaltyCard.Code)
            .Map(dest => dest.Discounts, src => src.Discounts);

        config.NewConfig<CreateLoyaltyCardRequest, CreateLoyaltyCardCommand>()
            .Map(dest => dest.CustomerId, src => src.CustomerId);
        
        config.NewConfig<CreateLoyaltyCardResult, CreateLoyaltyCardResponse>()
            .Map(dest => dest.LoyaltyCardId, src => src.LoyaltyCard.Id.Value)
            .Map(dest => dest.CustomerId, src => src.LoyaltyCard.CustomerId.Value)
            .Map(dest => dest.LoyaltyCardCode, src => src.LoyaltyCard.Code);

        config.NewConfig<(int id, AddLoyaltyCardDiscountsRequest request), AddLoyaltyCardDiscountsCommand>()
            .Map(dest => dest.LoyaltyCardId, src => src.id)
            .Map(dest => dest.DiscountIds, src => src.request.DiscountIds);

        config.NewConfig<AddLoyaltyCardDiscountsResult, AddLoyaltyCardDiscountsResponse>()
            .Map(dest => dest.LoyaltyCardId, src => src.LoyaltyCard.Id.Value)
            .Map(dest => dest.CustomerId, src => src.LoyaltyCard.CustomerId.Value)
            .Map(dest => dest.LoyaltyCardCode, src => src.LoyaltyCard.Code)
            .Map(dest => dest.Discounts, src => src.Discounts);
    }
}
