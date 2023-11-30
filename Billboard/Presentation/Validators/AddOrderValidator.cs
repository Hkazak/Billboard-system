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
        var (startDate, endDate) = (DateTime.UnixEpoch, DateTime.UnixEpoch);
        RuleFor(e => e.StartDate)
            .Must(e => DateTime.TryParseExact(e, FormatConstants.ValidDateFormat, null, DateTimeStyles.None,
                out startDate));
        RuleFor(e => e.EndDate)
            .Must(e => DateTime.TryParseExact(e, FormatConstants.ValidDateFormat, null, DateTimeStyles.None, out endDate));
        if (startDate == DateTime.UnixEpoch || endDate == DateTime.UnixEpoch) return;
        RuleFor(x => startDate)
            .GreaterThan(x => endDate);
        RuleFor(x => x).MustAsync((e, token) =>
            context.IsNotIntersectAsync(startDate, endDate, e.BillboardId, e.TariffId, token));
    }
}