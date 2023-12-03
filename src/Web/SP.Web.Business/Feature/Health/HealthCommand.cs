using MediatR;

namespace SP.Web.Business.Feature.Health;

public class HealthCommand : IRequest<string>
{
    public HealthCommand(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}