using MediatR;
using SP.Shared.Common.Feature.Event.Dto;

namespace Sp.Api.Business.Feature.Event.GetEvents;

public class GetEventsCommand : IRequest<List<EventDto>>
{
    public GetEventsCommand(Guid currentUserId)
    {
        CurrentUserId = currentUserId;
    }

    public Guid CurrentUserId { get; set; }
}