using System.Threading.Tasks;
using BugOsPos.Application.Products;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BugOsPos.Contracts.Products;
using BugOsPos.Infrastructure.Authentication;
using BugOsPos.Domain.Common.ErrorsCollection;

namespace BugOsPos.Api.Controllers;

[ApiController]
public sealed class ProductsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    
    public ProductsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [HttpGet("products/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProduct(int id)
    {
        
        var command = new GetProductByIdQuery(id);
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<GetProductByIdResponse>(result)),
            errors => Problem(errors));
    }
    
    [HttpGet("products")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProducts()
    {
        
        var command = new GetProductsQuery();
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<GetProductsResponse>(result)),
            errors => Problem(errors));
    }
}
