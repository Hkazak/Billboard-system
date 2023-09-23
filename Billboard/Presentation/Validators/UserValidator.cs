using System.Text.RegularExpressions;
using Contracts.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Presentation.Validators;

public class UserValidator : AbstractValidator<SignupRequest> 
{
    private readonly BillboardContext _context;
    
    public UserValidator(BillboardContext context)
    {
        _context = context;
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().Matches(new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"));
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MustAsync(IsUniqueEmailAsync);
    }

    private async Task<bool> IsUniqueEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(e => e.Email == email, cancellationToken);
    }
}