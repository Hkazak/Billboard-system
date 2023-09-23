using Application.CQRS.Commands;
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
    [Route("signup")]
    public async Task<ActionResult<AuthTokenResponse>> SignupUser([FromBody] SignupRequest request)
    {
        var validatorResult =await _validator.ValidateAsync(request, CancellationToken.None);
        if (!validatorResult.IsValid)
        {
            return BadRequest(); 
        }
        var command = new AddUserCommand
        {
            Request = request
        };
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}