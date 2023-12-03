using MediatR;
using Microsoft.Extensions.Logging;
using Sp.Api.Client.Feature.Health;

namespace SP.Web.Business.Feature.Health;

public class HealthCommandHandler : IRequestHandler<HealthCommand, string>
{
    private readonly IApiHealthClient _apiHealthClient;
    private readonly ILogger<HealthCommandHandler> _logger;

    public HealthCommandHandler(IApiHealthClient apiHealthClient, ILogger<HealthCommandHandler> logger)
    {
        _apiHealthClient = apiHealthClient;
        _logger = logger;
    }

    public async Task<string> Handle(HealthCommand request, CancellationToken cancellationToken)
    {
        var health = "Healt check: " + DateTime.UtcNow.ToString();
        var apiHealth = await GetApiHealth();
        var secureApiHealth = await GetSecureApiHealth(request.UserId);

        return health + " - " + apiHealth + " - " + secureApiHealth;
    }

    private async Task<string> GetSecureApiHealth(string userId)
    {
        var secureApiHealth = "No secureApiHealth connection";
        try
        {
            secureApiHealth = await _apiHealthClient.SecureHealth(userId);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
        }

        return secureApiHealth;
    }

    private async Task<string> GetApiHealth()
    {
        var apiHealth = "No api connection";
        try
        {
            apiHealth = await _apiHealthClient.Health();
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
        }

        return apiHealth;
    }
}