using Application.Regexes;
using Contracts.Constants;
using Contracts.Requests;
using FluentValidation;
using Persistence.Context;
using Persistence.Extensions;

namespace Presentation.Validators;

public class SignupValidator : AbstractValidator<SignupRequest> 
{
    public SignupValidator(BillboardContext context)
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage(ValidationErrorMessages.NameIsEmpty);
        RuleFor(x => x.Password).NotEmpty()
            .Matches(PasswordRegexes.Length8AtLeastOneCharAndDigitPasswordRegex())
            .WithMessage(ValidationErrorMessages.InvalidPasswordFormat);
        RuleFor(x => x.Email).NotEmpty()
            .EmailAddress()
            .WithMessage(ValidationErrorMessages.InvalidEmailFormat)
            .MustAsync(context.IsUniqueEmailAsync)
            .WithMessage(ValidationErrorMessages.EmailAlreadyUsed);
    }
}