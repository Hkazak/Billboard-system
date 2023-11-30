using System.ComponentModel;
using Contracts.Constants;

namespace Contracts.Responses;

public record OrderResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public DiscountResponse? Discount { get; init; }
    public required TariffResponse Tariff { get; init; }

    [DefaultValue(FormatConstants.ValidDateFormat)]
    public required string StartDate { get; init; }

    [DefaultValue(FormatConstants.ValidDateFormat)]
    public required string EndDate { get; init; }

    public required decimal RentPrice { get; init; }
    public required decimal ProductPrice { get; init; }
    public required decimal PenaltyPrice { get; init; }
    public required ICollection<string> UploadedFiles { get; init; }
    public required string BillboardSurface { get; init; }
    public required decimal Width { get; init; }
    public required decimal Height { get; init; }
    public required string BillboardType { get; init; }
}