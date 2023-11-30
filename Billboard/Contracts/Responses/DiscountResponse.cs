using System.ComponentModel;
using Contracts.Constants;

namespace Contracts.Responses;

public record DiscountResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required decimal SalesOf { get; init; }
    public required int MinRentCount { get; init; }

    [DefaultValue(FormatConstants.ValidDateFormat)]
    public required string EndDate { get; init; }

    public ICollection<ShortBillboardResponse> Billboards { get; init; } = new List<ShortBillboardResponse>();
}