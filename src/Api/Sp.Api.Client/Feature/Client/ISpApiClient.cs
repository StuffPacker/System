namespace Sp.Api.Client.Feature.Client;

public interface ISpApiClient
{
    Task<string> Get(string url);
}