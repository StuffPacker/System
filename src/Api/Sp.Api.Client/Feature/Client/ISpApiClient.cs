using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Client.Feature.Client;

public interface ISpApiClient
{
    Task<string> Get(string url);

    Task<string?> GetSecure(string securehealth, string userId);

    Task<string> PostSecure(string url, string userId, object dto);

    Task DeleteSecure(string url, string userId);

    Task<string> PatchSecure(string url, string userId, object dto);

    Task<string> PutSecure(string url, string userId, object dto);
}