using System.Net;
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Contracts.Requests;
using Contracts.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Validators;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]

public class DiscountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddDiscountRequest> _addDiscountValidator;

    public DiscountController(IMediator mediator, IValidator<AddDiscountRequest> addDiscountValidator)
    {
        _mediator = mediator;
        _addDiscountValidator = addDiscountValidator;
    }
    
    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult<DiscountResponse>> createDiscount([FromBody] AddDiscountRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var validationResult = await _addDiscountValidator.ValidateAsync(request, cancellationToken);
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

        var command = new AddDiscountCommand
        {
            Request = request
        };
        var response = _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetDiscount), new
        {
            id = response.Id
        }, response);

    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<DiscountResponse>> GetDiscount([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetDiscountQuery
        {
            Id = id
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<ActionResult<DiscountResponse>> GetDiscountList()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetDiscountListQuery();
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpDelete]
    [Authorize(Roles = "Manager")]
    [Route("{id:guid}")]
    public async Task<ActionResult> DeleteDiscount([FromBody] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new DeleteDiscountCommand
        {
            DiscountId = id
        };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}