using BugOsPos.Api.Http;
using BugOsPos.Domain.Common.ErrorsCollection;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace BugOsPos.Api.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected string? GetClaimValue(string claimKey)
    {
        return HttpContext.User.Claims
            .Where(claim => claim.Type == claimKey)
            .Select(claim => claim.Value)
            .FirstOrDefault();
    }

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
            return Problem();

        if (errors.All(error => error.Type is ErrorType.Validation))
            return ValidationProblem(errors);


        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        return Problem(errors[0]);

    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
            modelStateDictionary.AddModelError(error.Code, error.Description);

        return ValidationProblem(modelStateDictionary);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = (int)error.Type switch
        {
            (int)ErrorType.Conflict => StatusCodes.Status409Conflict,
            (int)ErrorType.Validation => StatusCodes.Status400BadRequest,
            (int)ErrorType.NotFound => StatusCodes.Status404NotFound,
            (int)CustomErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            (int)CustomErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }
}
