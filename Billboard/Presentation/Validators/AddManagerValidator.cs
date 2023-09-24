using Application.Regexes;
using Contracts.Requests;
using Contracts.ValidationMessages;
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
            .WithMessage(ValidationErrorMessages.InvalidEmailFormat)
            .MustAsync(context.IsUniqueEmailAsync)
            .WithMessage(ValidationErrorMessages.EmailAlreadyUsed);
        RuleFor(e => e.Password)
            .NotEmpty()
            .Matches(PasswordRegexes.Length8AtLeastOneCharAndDigitPasswordRegex())
            .WithMessage(ValidationErrorMessages.InvalidPasswordFormat);
    }
}