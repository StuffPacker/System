namespace Sp.Api.Client.Feature.Health;

public interface IApiHealthClient
{
    public Task<string> Health();
}