using System.ComponentModel;
using Contracts.Constants;

namespace Contracts.Requests;

public record RecalculateOrderPriceRequest
{
    [DefaultValue(FormatConstants.ValidDateFormat)]
    public required string StartDate { get; init; }

    [DefaultValue(FormatConstants.ValidDateFormat)]
    public required string EndDate { get; init; }
}