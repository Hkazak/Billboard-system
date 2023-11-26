using Application.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class BillboardTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BillboardTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> BillboardTypesList()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new GetBillboardTypesQuery();
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
}