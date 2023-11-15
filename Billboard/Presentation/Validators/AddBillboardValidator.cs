using Contracts.Constants;
using Contracts.Requests;
using FluentValidation;
using Persistence.Context;
using Persistence.Models;

namespace Presentation.Validators;

public class AddBillboardValidator : AbstractValidator<AddBillboardRequest>
{
    public AddBillboardValidator(BillboardContext context)
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage(ValidationErrorMessages.NameIsEmpty);
        RuleFor(x => x.Description).NotEmpty()
            .WithMessage(ValidationErrorMessages.DescriptionIsEmpty);
        RuleFor(x => x.Address).NotEmpty()
            .WithMessage(ValidationErrorMessages.AddressIsEmpty);
        RuleFor(x => x.Height).NotEmpty()
            .WithMessage(ValidationErrorMessages.HeightIsEmpty);
        RuleFor(x => x.Width).NotEmpty()
            .WithMessage(ValidationErrorMessages.WidthIsEmpty);
    }
}