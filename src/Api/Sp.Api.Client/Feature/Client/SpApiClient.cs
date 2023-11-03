using System.Text.Json;

namespace Sp.Api.Client.Feature.Client;

public class SpApiClient : ISpApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SpApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> Get(string url)
    {
        var client = _httpClientFactory.CreateClient("SpApi");
        var httpResponseMessage = await client.GetAsync(url);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
             var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();

             return contentStream;
        }

        throw new InvalidOperationException();
    }
}