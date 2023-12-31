﻿using System.Net;
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
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult<PriceRuleResponse>> CreatePriceRule([FromBody] AddPriceRuleRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errorResponse = new ErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = string.Concat(validationResult.Errors.Select(e=>e.ErrorMessage))
            };
            return BadRequest(errorResponse);
        }

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