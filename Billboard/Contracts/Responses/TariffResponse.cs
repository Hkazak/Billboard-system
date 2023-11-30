using System.ComponentModel;
using Contracts.Constants;

namespace Contracts.Responses;

public record TariffResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }

    [DefaultValue(FormatConstants.ValidTimeFormat)]
    public required string StartTime { get; init; }

    [DefaultValue(FormatConstants.ValidTimeFormat)]
    public required string EndTime { get; init; }

    public required decimal Price { get; init; }
}