using System.Net;
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Contracts.Requests;
using Contracts.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupOfTariffsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddGroupOfTariffsRequest> _addGroupOfTariffsValidator;

    public GroupOfTariffsController(IMediator mediator, IValidator<AddGroupOfTariffsRequest> addGroupOfTariffsValidator)
    {
        _mediator = mediator;
        _addGroupOfTariffsValidator = addGroupOfTariffsValidator;
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult<GroupOfTariffsResponse>> CreateGroupOfTariffs(
        [FromBody] AddGroupOfTariffsRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var validationResult = await _addGroupOfTariffsValidator.ValidateAsync(request, cancellationToken);
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

        var command = new AddGroupOfTariffsCommand
        {
            Request = request
        };
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<GroupOfTariffsResponse>> GetGroupOfTariffs([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetGroupOfTariffsQuery
        {
            Id = id
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GroupOfTariffsResponse>> GetGroupOfTariffsList()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetGroupOfTariffsListQuery();
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpDelete]
    [Authorize(Roles = "Manager")]
    [Route("{id:guid}")]
    public async Task<ActionResult> DeleteGroupOfTariffs([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new DeleteGroupOfTariffsCommand
        {
            GroupId = id
        };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}