using System.ComponentModel;
using Contracts.Constants;

namespace Contracts.Requests;

public record AddOrderRequest
{
    public required Guid BillboardId { get; init; }

    [DefaultValue(FormatConstants.ValidDateFormat)]
    public required string StartDate { get; init; }

    [DefaultValue(FormatConstants.ValidDateFormat)]
    public required string EndDate { get; init; }

    public required Guid TariffId { get; init; }
    public ICollection<string> Files { get; init; } = new List<string>();
}