using Contracts.Constants;
using Contracts.Requests;
using FluentValidation;
using Persistence.Context;

namespace Presentation.Validators;

public class AddBillboardSurfaceValidator : AbstractValidator<AddBillboardSurfaceRequest>
{
    public AddBillboardSurfaceValidator(BillboardContext context)
    {
        RuleFor(x => x.Surface).NotEmpty()
            .WithMessage(ValidationErrorMessages.SurfaceIsEmpty);
    }
}