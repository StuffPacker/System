using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SP.Shared.Common.Feature.Jwt;

namespace Sp.Api.Client.Feature.Client;

public class SpApiClient : ISpApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JwtOptions _jwtOptions;

    public SpApiClient(IHttpClientFactory httpClientFactory, IOptions<JwtOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _jwtOptions = options.Value;
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

    public async Task<string> GetSecure(string url, string userId)
    {
        var client = SecureClient(userId);
        var httpResponseMessage = await client.GetAsync(url);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();

            return contentStream;
        }

        throw new InvalidOperationException();
    }

    private HttpClient SecureClient(string userId)
    {
        var client = _httpClientFactory.CreateClient("SpApi");

// add new security
        var token = GetToken(userId);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    private string GetToken(string userid)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>();
        claims.Add(new Claim("UserId", userid));
        var token = new JwtSecurityToken(
            "Stuffpacker.net",
            "Stuffpacker.net",
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}