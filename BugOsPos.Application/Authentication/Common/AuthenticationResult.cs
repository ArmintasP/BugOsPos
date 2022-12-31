using BugOsPos.Domain.CustomerAggregate;

namespace BugOsPos.Application.Authentication.Common;

public record CustomerAuthenticationResult(
    Customer Customer,
    string Token);
