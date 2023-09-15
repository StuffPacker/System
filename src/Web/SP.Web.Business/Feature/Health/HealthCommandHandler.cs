using MediatR;

namespace SP.Web.Business.Feature.Health;

public class HealthCommandHandler : IRequestHandler<HealthCommand, string>
{
    public async Task<string> Handle(HealthCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        return "Healt check: " + DateTime.UtcNow.ToString();
    }
}