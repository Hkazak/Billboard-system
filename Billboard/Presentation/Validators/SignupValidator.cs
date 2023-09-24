using Application.Regexes;
using Contracts.Requests;
using FluentValidation;
using Persistence.Context;
using Persistence.Extensions;

namespace Presentation.Validators;

public class SignupValidator : AbstractValidator<SignupRequest> 
{
    public SignupValidator(BillboardContext context)
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Password).NotEmpty()
            .Matches(PasswordRegexes.Length8AtLeastOneCharAndDigitPasswordRegex());
        RuleFor(x => x.Email).NotEmpty()
            .EmailAddress()
            .MustAsync(context.IsUniqueEmailAsync);
    }
}