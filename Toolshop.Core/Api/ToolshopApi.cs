using RestSharp;
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

        _client = new RestClient(options);
        Products = new ProductsClient(_client);
    }

    public ProductsClient Products { get; }

    public void Dispose()
    {
        _client.Dispose();
    }
}