using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Contracts.Requests;
using Contracts.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class PriceRulesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddPriceRuleRequest> _validator;

    public PriceRulesController(IMediator mediator, IValidator<AddPriceRuleRequest> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<PriceRuleResponse>> GetPriceRule([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetPriceRuleQuery
        {
            PriceRuleId = id
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PriceRuleResponse>>> GetPriceRuleList()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetPriceRuleListQuery();
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<PriceRuleResponse>> CreatePriceRule([FromBody] AddPriceRuleRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new AddPriceRuleCommand
        {
            Request = request
        };
        var response = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPriceRule), new
        {
            id = response.Id
        }, response);
    }
}