using Contracts.Requests;
using FluentValidation;
using Persistence.Context;
using Persistence.Extensions;

namespace Presentation.Validators;

public class AddOrderValidator : AbstractValidator<AddOrderRequest>
{
    public AddOrderValidator(BillboardContext context)
    {
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
        RuleFor(x => x).MustAsync((e, token) =>
            context.IsNotIntersectAsync(e.StartDate, e.EndDate, e.BillboardId, e.TariffId, token));
    }
}