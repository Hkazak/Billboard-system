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
public class BillboardsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddBillboardRequest> _addBillboardValidator;

    public BillboardsController(IMediator mediator, IValidator<AddBillboardRequest> addBillboardValidator)
    {
        _mediator = mediator;
        _addBillboardValidator = addBillboardValidator;
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult<BillboardResponse>> CreateBillboard([FromBody] AddBillboardRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var validationResult = await _addBillboardValidator.ValidateAsync(request, cancellationToken);
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

        var command = new AddBillboardCommand
        {
            Request = request
        };
        var response = _mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<BillboardResponse>> GetBillboard([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetBillboardInformationQuery
        {
            BillboardId = id
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<BillboardResponse>> GetBillboards()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetBillboardsInformationQuery();
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpDelete]
    [Authorize(Roles = "Manager")]
    [Route("{id:guid}")]
    public async Task<ActionResult> DeleteBillboard([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new DeleteBillboardCommand
        {
            BillboardId = id
        };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}