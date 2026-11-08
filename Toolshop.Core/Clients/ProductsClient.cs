using RestSharp;
using Toolshop.Core.Api.Models.Common;
using Toolshop.Core.Api.Models.Products;

namespace Toolshop.Core.Api.Clients;

public sealed class ProductsClient
{
    private readonly RestClient _client;

    public ProductsClient(RestClient client)
    {
        _client = client;
    }

    public Task<RestResponse<PagedResponse<Product>>> GetProductsAsync(int page = 1, CancellationToken ct = default)
    {
        var request = new RestRequest("products")
            .AddQueryParameter("page", page);

        return _client.ExecuteAsync<PagedResponse<Product>>(request, ct);
    }

    public Task<RestResponse<PagedResponse<Product>>> SearchProductsAsync(
        string query, int? page = null, CancellationToken ct = default)
    {
        var request = new RestRequest("products/search")
            .AddQueryParameter("q", query);

        if (page is not null)
            request.AddQueryParameter("page", page.Value);

        return _client.ExecuteAsync<PagedResponse<Product>>(request, ct);
    }

    public Task<RestResponse<PagedResponse<Product>>> GetProductByIdAsync(int id, CancellationToken ct = default)
    {
        var request = new RestRequest("products/{id}")
            .AddUrlSegment("id", id);

        return _client.ExecuteAsync<PagedResponse<Product>>(request, ct);
    }
}