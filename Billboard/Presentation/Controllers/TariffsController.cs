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
public class TariffsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddTariffRequest> _addTariffValidator;

    public TariffsController(IMediator mediator, IValidator<AddTariffRequest> addTariffValidator)
    {
        _mediator = mediator;
        _addTariffValidator = addTariffValidator;
    }
    
    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult<TariffResponse>> CreateTariff([FromBody] AddTariffRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var validationResult = await _addTariffValidator.ValidateAsync(request, cancellationToken);
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
        
        var command = new AddTariffCommand
        {
            Request = request
        };
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<TariffResponse>> GetTariff([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetTariffInformationQuery
        {
            TariffId = id
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<TariffResponse>> GetTariffs()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetTariffsInformationQuery();
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpDelete]
    [Authorize(Roles = "Manager")]
    [Route("{id:guid}")]
    public async Task<ActionResult> DeleteTariff([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new DeleteTariffCommand
        {
            TariffId = id
        };

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}