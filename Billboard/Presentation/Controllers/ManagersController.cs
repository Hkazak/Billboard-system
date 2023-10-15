using System.Net;
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Contracts.Requests;
using Contracts.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ManagersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddManagerRequest> _validator;

    public ManagersController(IMediator mediator, IValidator<AddManagerRequest> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ManagerResponse>> CreateManager([FromBody] AddManagerRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            var errorResponse = new ErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = errorMessage
            };
            return BadRequest(errorResponse);
        }

        var command = new AddManagerCommand
        {
            Request = request
        };
        var response = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetManager), new
        {
            id = response.Id
        }, response);
    }
    
    [HttpPost]
    [Route("sign-in")]
    public async Task<ActionResult<AuthTokenResponse>> SigninManager([FromBody] SigninRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new SigninManagerQuery()
        {
            Request = request
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<ManagerResponse>> GetManager([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetManagerInformationQuery
        {
            ManagerId = id
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpDelete]
    [Authorize(Roles = "Administrator")]
    [Route("{id:guid}")]
    public async Task<ActionResult> DeleteManager([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new DeleteManagerCommand
        {
            ManagerId = id
        };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}