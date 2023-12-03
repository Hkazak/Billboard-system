using System.Globalization;
using Contracts.Constants;
using Contracts.Requests;
using FluentValidation;
using Persistence.Context;
using Persistence.Extensions;

namespace Presentation.Validators;

public class AddOrderValidator : AbstractValidator<AddOrderRequest>
{
    public AddOrderValidator(BillboardContext context)
    {
        RuleFor(e => e.StartDate)
            .Must(e => DateTime.TryParseExact(e, FormatConstants.ValidDateFormat, null, DateTimeStyles.None, out _));
        RuleFor(e => e.EndDate)
            .Must(e => DateTime.TryParseExact(e, FormatConstants.ValidDateFormat, null, DateTimeStyles.None, out _));
        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate);
        RuleFor(x => x).MustAsync((e, token) =>
            context.IsNotIntersectAsync(
                DateTime.ParseExact(e.StartDate, FormatConstants.ValidDateFormat, null, DateTimeStyles.None).ToUniversalTime(),
                DateTime.ParseExact(e.EndDate, FormatConstants.ValidDateFormat, null, DateTimeStyles.None).ToUniversalTime(),
                e.BillboardId, e.TariffId, token));
    }
}