using Application.Regexes;
using Contracts.Requests;
using FluentValidation;
using Persistence.Context;
using Persistence.Extensions;

namespace Presentation.Validators;

public class AddManagerValidator : AbstractValidator<SigninRequest>
{
    public AddManagerValidator(BillboardContext context)
    {
        RuleFor(e => e.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(context.IsUniqueEmailAsync);
        RuleFor(e => e.Password)
            .NotEmpty()
            .Matches(PasswordRegexes.Length8AtLeastOneCharAndDigitPasswordRegex());
    }
}