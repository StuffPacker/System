using MediatR;
using SP.Shared.Common.Feature.Event.Model;

namespace SP.Web.Business.Feature.Event.GetEvents;

public class GetEventsCommand : IRequest<List<EventModel>>
{
    public GetEventsCommand(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}