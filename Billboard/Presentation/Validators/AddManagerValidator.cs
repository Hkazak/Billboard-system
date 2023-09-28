using Application.Regexes;
using Contracts.Constants;
using Contracts.Requests;
using FluentValidation;
using Persistence.Context;
using Persistence.Extensions;

namespace Presentation.Validators;

public class AddManagerValidator : AbstractValidator<AddManagerRequest>
{
    public AddManagerValidator(BillboardContext context)
    {
        RuleFor(x => x.FirstName).NotEmpty()
            .WithMessage(ValidationErrorMessages.NameIsEmpty);
        RuleFor(x => x.Email).NotEmpty()
            .EmailAddress()
            .WithMessage(ValidationErrorMessages.InvalidEmailFormat)
            .MustAsync(context.IsUniqueEmailAsync)
            .WithMessage(ValidationErrorMessages.EmailAlreadyUsed);
        RuleFor(x => x.Phone).NotEmpty()
            .Matches(ValidationRegexes.PhoneNumberRegex())
            .WithMessage(ValidationErrorMessages.InvalidPhoneNumber)
            .MustAsync(context.IsUniquePhoneAsync)
            .WithMessage(ValidationErrorMessages.PhoneAlreadyUsed);
    }
}