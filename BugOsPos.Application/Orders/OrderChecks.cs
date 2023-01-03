using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate;
using ErrorOr;

namespace BugOsPos.Application.Orders;

public static class OrderChecks
{
    public static async Task<ErrorOr<bool>> IsValidCaller(Order order, (int? CustomerId, int? EmployeeId) request, IEmployeeRepository employeeRepository)
    {
        if (request.CustomerId is int requestCustomerId)
        {
            var customerId = CustomerId.New(requestCustomerId);

            if (!customerId.Equals(order.CustomerId))
                return Domain.Common.ErrorsCollection.Errors.Order.NotFound;
        }

        else if (request.EmployeeId is int requestEmployeeId)
        {
            var employeeId = EmployeeId.New(requestEmployeeId);
            var employee = await employeeRepository.GetEmployeeById(employeeId);

            if (order.CashierId is EmployeeId orderCashierId)
            {
                var cashier = await employeeRepository.GetEmployeeById(orderCashierId);
                if (cashier?.GroupId is null || !cashier.GroupId.Equals(employee?.GroupId))
                    return Domain.Common.ErrorsCollection.Errors.Order.NotFound;
            }

            if (order.CourierId is EmployeeId orderCourierId)
            {
                var courier = await employeeRepository.GetEmployeeById(orderCourierId);
                if (courier?.GroupId is null || !courier.GroupId.Equals(employee?.GroupId))
                    return Domain.Common.ErrorsCollection.Errors.Order.NotFound;
            }
        }

        return true;
    }
}