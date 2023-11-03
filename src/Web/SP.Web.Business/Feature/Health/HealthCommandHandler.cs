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
        var apiHealth = "No api connection";
        try
        {
            apiHealth = await _apiHealthClient.Health();
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
        }

        return health + " - " + apiHealth;
    }
}