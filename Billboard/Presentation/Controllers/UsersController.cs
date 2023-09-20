using Application.CQRS.Commands;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("signup")]
    public async Task<ActionResult<AuthTokenResponse>> SignupUser([FromBody] SignupRequest request)
    {
        var command = new AddUserCommand
        {
            Request = request
        };
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}