using Application.Regexes;
using Contracts.Requests;
using Contracts.ValidationMessages;
using FluentValidation;
using Persistence.Context;
using Persistence.Extensions;

namespace Presentation.Validators;

public class AddManagerValidator : AbstractValidator<AddManagerRequest>
{
    public AddManagerValidator(BillboardContext context)
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage(ValidationErrorMessages.NameIsEmpty);
        RuleFor(x => x.Email).NotEmpty()
            .EmailAddress()
            .WithMessage(ValidationErrorMessages.InvalidEmailFormat)
            .MustAsync(context.IsUniqueEmailAsync)
            .WithMessage(ValidationErrorMessages.EmailAlreadyUsed);
    }
}