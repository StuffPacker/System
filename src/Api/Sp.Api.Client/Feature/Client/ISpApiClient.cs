namespace Sp.Api.Client.Feature.Client;

public interface ISpApiClient
{
    Task<string> Get(string url);

    Task<string> GetSecure(string securehealth, string userId);
}