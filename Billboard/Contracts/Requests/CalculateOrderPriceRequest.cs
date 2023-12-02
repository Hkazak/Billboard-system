using System.ComponentModel;
using Contracts.Constants;

namespace Contracts.Requests;

public record CalculateOrderPriceRequest
{
    public required Guid BillboardId { get; init; }
    public required Guid TariffId { get; init; }

    [DefaultValue(FormatConstants.ValidDateFormat)]
    public required string StartDate { get; init; }

    [DefaultValue(FormatConstants.ValidDateFormat)]
    public required string EndDate { get; init; }
}