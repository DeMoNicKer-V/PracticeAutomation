using RestSharp;
using RestSharp.Serializers;
using RestSharp.Serializers.Json;
using Toolshop.Core.Api.Clients;

namespace Toolshop.Core.Api;

public sealed class ToolshopApi : IDisposable
{
    private readonly RestClient _client;

    public ToolshopApi(string baseUrl)
    {
        var options = new RestClientOptions(baseUrl)
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        _client = new RestClient(
            options,
            configureSerialization: s => s.UseOnlySerializer(() => new SystemTextJsonSerializer()));
        Products = new ProductsClient(_client);
    }

    public ProductsClient Products { get; }

    public void Dispose()
    {
        _client.Dispose();
    }
}