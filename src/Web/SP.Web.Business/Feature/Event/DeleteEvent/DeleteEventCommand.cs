using MediatR;

namespace SP.Web.Business.Feature.Event.DeleteEvent;

public class DeleteEventCommand : IRequest<string>
{
    public DeleteEventCommand(string id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    public Guid UserId { get; set; }

    public string Id { get; set; }
}