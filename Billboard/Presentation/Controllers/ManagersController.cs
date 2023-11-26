using System.Net;
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.Extensions;
using Contracts.Requests;
using Contracts.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ManagersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddManagerRequest> _addManagerValidator;
    private readonly IValidator<ChangePasswordRequest> _changePasswordValidator;

    public ManagersController(IMediator mediator, IValidator<AddManagerRequest> addManagerValidator,
        IValidator<ChangePasswordRequest> changePasswordValidator)
    {
        _mediator = mediator;
        _addManagerValidator = addManagerValidator;
        _changePasswordValidator = changePasswordValidator;
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ManagerResponse>> CreateManager([FromBody] AddManagerRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var validationResult = await _addManagerValidator.ValidateAsync(request, cancellationToken);
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

    [HttpGet]
    public async Task<ActionResult<ManagerResponse>> GetManagers()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetManagersInformationQuery();
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
    
    [HttpPut]
    [Route("password")]
    public async Task<ActionResult> UpdatePassword([FromBody] ChangePasswordRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var validationResult = await _changePasswordValidator.ValidateAsync(request, cancellationToken);
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

        var command = new ChangeManagerPasswordCommand
        {
            NewData = request.CreateUserPasswordData(User.GetUserId())
        };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
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

    [HttpPatch]
    [Route("activate/{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> ActivateManager([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new ActivateManagerCommand
        {
            ManagerId = id
        };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}