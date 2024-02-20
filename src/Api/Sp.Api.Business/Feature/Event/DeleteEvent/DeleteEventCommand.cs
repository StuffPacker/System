using MediatR;

namespace Sp.Api.Business.Feature.Event.DeleteEvent;

public class DeleteEventCommand : IRequest<string>
{
    public DeleteEventCommand(string id, Guid currentUser)
    {
        Id = id;
        CurrentUser = currentUser;
    }

    public Guid CurrentUser { get; set; }

    public string Id { get; set; }
}