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
    private readonly IValidator _validator;

    public ManagersController(IMediator mediator, IValidator validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<UserResponse>> CreateManager([FromBody] AddManagerRequest request)
    {
        var validationContext = new ValidationContext<AddManagerRequest>(request);
        var validationResult = await _validator.ValidateAsync(validationContext);
        if (validationResult.IsValid)
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
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetManager), new
        {
            id = response.Id
        });
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<UserResponse>> GetManager([FromRoute] Guid id)
    {
        var query = new GetUserInformationQuery
        {
            UserId = id
        };
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}