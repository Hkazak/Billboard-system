using Contracts.Requests;
using FluentValidation;
using Persistence.Context;
using Persistence.Enums;
using Persistence.Extensions;

namespace Presentation.Validators;

public class AddPriceRuleValidator : AbstractValidator<AddPriceRuleRequest>
{
    public AddPriceRuleValidator(BillboardContext context)
    {
        RuleFor(e => e.Price).GreaterThan(0);
        RuleFor(e => e.BillboardType).Must(e => Enum.IsDefined(typeof(BillboardTypeId), e));
        RuleFor(e => e).MustAsync((e, token) =>
            context.IsUniquePriceRuleAsync(e.BillboardSurfaceId, Enum.Parse<BillboardTypeId>(e.BillboardType), token));
    }
}