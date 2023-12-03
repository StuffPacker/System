namespace Sp.Api.Client.Feature.Health;

public interface IApiHealthClient
{
    Task<string> Health();

    Task<string> SecureHealth(string userId);
}