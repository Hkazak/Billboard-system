using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    public async Task<ActionResult<PaymentResponse>> CreatePayment([FromBody] CreatePaymentRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new CreatePaymentCommand
        {
            Request = request
        };
        var response = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPayment), new
        {
            id = response.Id
        }, response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<PaymentResponse>> GetPayment([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetPaymentQuery
        {
            PaymentId = id
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Route("success/{id:guid}")]
    public async Task<ActionResult<PaymentResponse>> SuccessPayment([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new UpdatePaymentStatusCommand
        {
            OrderId = id
        };
        await _mediator.Send(query, cancellationToken);
        return Redirect("");
    }

    [HttpGet]
    [Route("failure/{id:guid}")]
    public async Task<ActionResult<PaymentResponse>> FailurePayment([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new UpdatePaymentStatusCommand
        {
            OrderId = id
        };
        await _mediator.Send(query, cancellationToken);
        return Redirect("");
    }
}