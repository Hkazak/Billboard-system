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

public class BillboardSurfaceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddBillboardSurfaceRequest> _addBillboardSurfaceValidator;

    public BillboardSurfaceController(IMediator mediator, IValidator<AddBillboardSurfaceRequest> addBillboardSurfaceValidator)
    {
        _mediator = mediator;
        _addBillboardSurfaceValidator = addBillboardSurfaceValidator;
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult<BillboardSurfaceResponse>> CreateBillboardSurface(
        [FromBody] AddBillboardSurfaceRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var validationResult = await _addBillboardSurfaceValidator.ValidateAsync(request, cancellationToken);
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

        var command = new AddBillboardSurfaceCommand
        {
            Request = request
        };
        var response = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetBillboardSurface), new
        {
            id = response.Id
        }, response);
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<BillboardSurfaceResponse>> GetBillboardSurface([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetBillboardSurfaceQuery
        {
            Id = id
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BillboardSurfaceResponse>>> GetBillboardSurfaceList()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetBillboardSurfaceListQuery();
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
}