using Sp.Api.Client.Feature.Client;

namespace Sp.Api.Client.Feature.Health;

public class ApiHealthClient : IApiHealthClient
{
    private readonly ISpApiClient _apiClient;

    public ApiHealthClient(ISpApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<string> Health()
    {
        var result = await _apiClient.Get("health");
        return result;
    }

    public async Task<string> SecureHealth(string userId)
    {
        var result = await _apiClient.GetSecure("secureHealth", userId);
        return result!;
    }
}