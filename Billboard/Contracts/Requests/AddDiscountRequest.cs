using System.ComponentModel;
using Contracts.Constants;

namespace Contracts.Requests;

public record AddDiscountRequest
{
    public required string Name { get; init; }
    public required decimal DiscountPercentage { get; init; }
    public required int MinRentCount { get; init; }
    [DefaultValue(FormatConstants.ValidDateFormat)]
    public required string EndDate { get; init; }
    public ICollection<Guid> BillboardIds { get; init; } = new List<Guid>();
}