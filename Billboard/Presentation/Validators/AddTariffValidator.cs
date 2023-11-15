using Contracts.Constants;
using Contracts.Requests;
using FluentValidation;
using Persistence.Context;

namespace Presentation.Validators;

public class AddTariffValidator : AbstractValidator<AddTariffRequest>
{
    public AddTariffValidator(BillboardContext context)
    {
        RuleFor(e => e.Title).NotEmpty()
            .WithMessage(ValidationErrorMessages.TitleIsEmpty);
        RuleFor(e => e.Price).NotEmpty()
            .WithMessage(ValidationErrorMessages.PriceIsEmpty);
        RuleFor(e => e.EndTime).NotEmpty()
            .WithMessage(ValidationErrorMessages.EndTimeIsEmpty);
        RuleFor(e => e.StartTime).NotEmpty()
            .WithMessage(ValidationErrorMessages.StartTimeIsEmpty);
    }
}