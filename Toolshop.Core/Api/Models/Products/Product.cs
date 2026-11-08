using System.Text.Json.Serialization;
using Toolshop.Core.Api.Models.Brands;
using Toolshop.Core.Api.Models.Categories;

namespace Toolshop.Core.Api.Models.Products;

public record Product
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("price")]
    public decimal Price { get; init; }

    [JsonPropertyName("is_location_offer")]
    public int IsLocationOffer { get; init; }

    [JsonPropertyName("is_rental")]
    public int IsRental { get; init; }

    [JsonPropertyName("in_stock")]
    public int InStock { get; init; }

    [JsonPropertyName("co2_rating")]
    public char Co2Rating { get; init; }

    [JsonPropertyName("is_eco_friendly")]
    public bool IsEcoFriendly { get; init; }

    [JsonPropertyName("brand")]
    public Brand Brand { get; init; }

    [JsonPropertyName("category")]
    public Category Category { get; init; }

    [JsonPropertyName("product_image")]
    public ProductImage ProductImage { get; init; }
}