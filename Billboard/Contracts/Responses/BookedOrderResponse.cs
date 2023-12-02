using System.ComponentModel;
using Contracts.Constants;

namespace Contracts.Responses;

public record BookedOrderResponse
{
    public required Guid OrderId { get; init; }
    
    [DefaultValue($"{FormatConstants.ValidDateFormat}")]
    public required string StartDate { get; init; }

    [DefaultValue($"{FormatConstants.ValidDateFormat}")]
    public required string EndDate { get; init; }
}