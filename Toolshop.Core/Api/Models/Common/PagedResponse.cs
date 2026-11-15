using System.Text.Json.Serialization;

namespace Toolshop.Core.Api.Models.Common;

public record PagedResponse<T>
{
    [JsonPropertyName("current_page")]
    public required int CurrentPage { get; init; }

    [JsonPropertyName("data")]
    public required IReadOnlyList<T> Data { get; init; }

    [JsonPropertyName("from")]
    public int? From { get; init; }

    [JsonPropertyName("last_page")]
    public required int LastPage { get; init; }

    [JsonPropertyName("per_page")]
    public required int PerPage { get; init; }

    [JsonPropertyName("to")]
    public int? To { get; init; }

    [JsonPropertyName("total")]
    public required int Total { get; init; }
}