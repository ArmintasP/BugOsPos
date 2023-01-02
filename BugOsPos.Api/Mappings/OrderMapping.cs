using BugOsPos.Application.Employees;
using BugOsPos.Contracts.Common;
using BugOsPos.Contracts.Orders;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.Entities;
using Mapster;

namespace BugOsPos.Api.Mappings;

public class OrderMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderSection>()
            .Map(dest => dest.Id, src => src.Id.Get())
            .Map(dest => dest.CustomerId, src => src.CustomerId.Get())
            .Map(dest => dest.PaymentId, src => src.Payment.GetId())
            .Map(dest => dest.CashierId, src => src.CashierId.Get())
            .Map(dest => dest.CourierId, src => src.CourierId.Get())
            .Map(dest => dest.LocationId, src => src.LocationId.Get())
            .Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.OrderItems, src => src.OrderItems);

        config.NewConfig<OrderItem, OrderItemSection>()
            .Map(dest => dest.Id, src => src.Id.Get())
            .Map(dest => dest.ProductId, src => src.ProductId.Get())
            .Map(dest => dest.DiscountId, src => src.DiscountId.Get())
            .Map(dest => dest.Status, src => src.Status.ToString());

        config.NewConfig<GetOrderByIdResult, GetOrderByIdResponse>();
    }
}
