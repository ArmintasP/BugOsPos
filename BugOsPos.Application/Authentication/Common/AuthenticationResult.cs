﻿using BugOsPos.Domain.CustomerAggregate;

namespace BugOsPos.Application.Authentication.Common;

public record AuthenticationResult(
    Customer Customer,
    string Token);