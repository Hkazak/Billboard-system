using System.Net;
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.Extensions;
using Contracts.DataTransferObjects;
using Contracts.Requests;
using Contracts.Responses;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddOrderRequest> _addOrderValidator;

    public OrdersController(IMediator mediator, IValidator<AddOrderRequest> addOrderValidator)
    {
        _mediator = mediator;
        _addOrderValidator = addOrderValidator;
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

    [HttpPost]
    [Route("price")]
    public async Task<ActionResult<OrderPriceResponse>> CalculateOrderPrice([FromBody] CalculateOrderPriceRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new CalculateOrderPriceQuery
        {
            Request = request
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [Route("{id:guid}/price")]
    public async Task<ActionResult<OrderPriceResponse>> CalculateOrderPrice([FromRoute] Guid id,
        [FromBody] RecalculateOrderPriceRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var queryRequest = new RecalculateOrderPrice
        {
            OrderId = id,
            StartDate = request.StartDate.ToDate(),
            EndDate = request.EndDate.ToDate()
        };
        var query = new RecalculateOrderPriceQuery
        {
            Request = queryRequest
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("statuses")]
    public async Task<ActionResult<IEnumerable<string>>> GetOrderStatuses()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetOrdersStatusesQuery();
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
        var validationResult = await _addOrderValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorResponse = new ErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
            };
            return BadRequest(errorResponse);
        }
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

    [HttpPut]
    [Route("{id:guid}/approve")]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> ApproveOrder([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new ApproveOrderCommand
        {
            OrderId = id
        };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPut]
    [Route("{id:guid}/cancel")]
    [Authorize(Roles = "Client, Manager")]
    public async Task<ActionResult> CancelOrder([FromRoute] Guid id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new CancelOrderCommand
        {
            OrderId = id,
            RequestSenderId = User.GetUserId()
        };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPut]
    [Route("{id:guid}")]
    [Authorize(Roles = "Client")]
    public async Task<ActionResult> ChangeOrder([FromRoute] Guid id, [FromBody] RecalculateOrderPriceRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new ChangeOrderCommand
        {
            Request = new ChangeOrder
            {
                OrderId = id,
                StartDate = request.StartDate.ToDate(),
                EndDate = request.EndDate.ToDate(),
                RequestSenderId = User.GetUserId()
            }
        };
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }
}