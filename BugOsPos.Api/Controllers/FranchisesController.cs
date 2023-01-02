﻿using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BugOsPos.Application.Franchises;
using BugOsPos.Contracts.Franchises;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Infrastructure.Authentication;

namespace BugOsPos.Api.Controllers;

public sealed class FranchisesController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public FranchisesController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet("franchises/{id}")]
    public async Task<IActionResult> GetFranchise(int id)
    {
        var command = new GetFranchiseByIdQuery(id);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<GetFranchiseByIdResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("franchises")]
    public async Task<IActionResult> CreateFranchise(CreateFranchiseCommand request)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Errors.Employee.Forbidden });

        var result = await _mediator.Send(request);

        return result.Match(
            result => Ok(_mapper.Map<CreateFranchiseResponse>(result)),
            errors => Problem(errors));
    }

    [HttpPost("franchises/{id}/products")]
    public async Task<IActionResult> CreateProductForFranchise(int id, CreateProductForFranchiseRequest request)
    {
        if (GetClaimValue(JwtSettings.EmployeeClaim) is null)
            return Problem(new() { Errors.Employee.Forbidden });

        var command = _mapper.Map<CreateProductForFranchiseCommand>((request, id));
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<CreateProductForFranchiseResponse>(result)),
            errors => Problem(errors));
    }
}
