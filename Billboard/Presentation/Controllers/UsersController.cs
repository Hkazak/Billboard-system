using System.Net;
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.Extensions;
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
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<SignupRequest> _signupValidator;
    private readonly IValidator<ChangePasswordRequest> _changePasswordValidator;

    public UsersController(IMediator mediator, IValidator<SignupRequest> signupValidator,
        IValidator<ChangePasswordRequest> changePasswordValidator)
    {
        _mediator = mediator;
        _signupValidator = signupValidator;
        _changePasswordValidator = changePasswordValidator;
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<AuthTokenResponse>> SignupUser([FromBody] SignupRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var validationResult = await _signupValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var message = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            var error = new ErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = message
            };
            return BadRequest(error);
        }

        var command = new AddUserCommand
        {
            Request = request
        };
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<ActionResult<AuthTokenResponse>> SigninUser([FromBody] SigninRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var query = new SigninUserQuery
        {
            Request = request
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpPut]
    [Route("password/update")]
    public async Task<ActionResult> UpdatePassword([FromBody] ChangePasswordRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var validationResult = await _changePasswordValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var message = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            var error = new ErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = message
            };
            return BadRequest(error);
        }

        var command = new ChangeUserPasswordCommand
        {
            NewData = request.CreateUserPasswordData(User.GetUserId())
        };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpGet]
    [Authorize]
    [Route("self")]
    public async Task<ActionResult<UserResponse>> GetUser()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var userId = User.GetUserId();
        var query = new GetUserInformationQuery
        {
            UserId = userId
        };
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpPut]
    [Route("password/forgot")]
    public async Task<ActionResult> SendCode([FromBody] ForgotPasswordRequest request)
    {
        var query = new ResetPasswordQuery
        {
            Email = request.Email
        };
        await _mediator.Send(query);
        return NoContent();
    }

    [HttpPut]
    [Route("password/reset")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var command = new ResetPasswordCommand
        {
            CodeConfirmationRequest = request.CreateResetPasswordData()
        };
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}