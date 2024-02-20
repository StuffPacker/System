using MediatR;
using SP.Shared.Common.Feature.Event.Model;

namespace SP.Web.Business.Feature.Event.CreateEvent;

public class CreateEventCommand : IRequest<EventModel>
{
    public CreateEventCommand(Guid currentUser)
    {
        CurrentUser = currentUser;
    }

    public Guid CurrentUser { get; set; }
}