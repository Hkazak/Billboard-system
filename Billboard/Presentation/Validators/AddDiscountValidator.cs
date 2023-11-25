using System.Globalization;
using Contracts.Constants;
using Contracts.Requests;
using FluentValidation;

namespace Presentation.Validators;

public class AddDiscountValidator : AbstractValidator<AddDiscountRequest>
{
    public AddDiscountValidator()
    {
        RuleFor(e => e.Name).NotEmpty()
            .WithMessage(ValidationErrorMessages.NameIsEmpty);
        RuleFor(e => e.DiscountPercentage).NotEmpty()
            .WithMessage(ValidationErrorMessages.PriceIsEmpty);
        RuleFor(e => e.EndDate).NotEmpty()
            .Must(value => DateTime.TryParseExact(value, ValidationConstants.ValidDateFormat, null, DateTimeStyles.None, out _));
    }
}