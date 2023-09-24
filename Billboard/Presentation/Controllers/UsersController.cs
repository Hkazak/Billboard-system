using System.Net;
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Contracts.Requests;
using Contracts.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<SignupRequest> _validator;

    public UsersController(IMediator mediator, IValidator<SignupRequest> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<AuthTokenResponse>> SignupUser([FromBody] SignupRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var message = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            var error = new ErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = message
            };
            return BadRequest(error);
        }

        var command = new AddUserCommand
        {
            Request = request
        };
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<ActionResult<AuthTokenResponse>> SigninUser([FromBody] SigninRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new SigninQuery
        {
            Request = request
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
}