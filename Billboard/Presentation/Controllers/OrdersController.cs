using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.Extensions;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Enums;
using Presentation.Extensions;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("booked")]
    public async Task<ActionResult<IEnumerable<BookedOrderResponse>>> GetBookedByTariffDates([FromQuery] BookedByTariffRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetBookedByTariffOrderListQuery
        {
            Request = request
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    [Authorize(Roles = "Manager, Client")]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetOrderListQuery
        {
            RequestSenderId = User.GetUserId()
        };
        var orders = await _mediator.Send(query, cancellationToken);
        return Ok(orders);
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    public async Task<ActionResult<OrderResponse>> CreateOrder([FromBody] AddOrderRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new AddOrderCommand
        {
            Request = request.CreateAddOrder(User.GetUserId())
        };
        var response = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetOrder), new
        {
            id = response.Id
        }, response);
    }

    [HttpGet]
    [Authorize(Roles = "Manager, Client")]
    [Route("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> GetOrder([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetOrderQuery
        {
            OrderId = id,
            RequestSenderId = User.GetUserId()
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
}