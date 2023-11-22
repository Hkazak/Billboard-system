using Contracts.Constants;
using Contracts.Responses;
using FluentValidation;
using Persistence.Context;

namespace Presentation.Validators;

public class AddDiscountValidator : AbstractValidator<DiscountResponse>
{
    public AddDiscountValidator(BillboardContext context)
    {
        RuleFor(e => e.Name).NotEmpty()
            .WithMessage(ValidationErrorMessages.NameIsEmpty);
        RuleFor(e => e.SalesOf).NotEmpty()
            .WithMessage(ValidationErrorMessages.PriceIsEmpty);
    }
}