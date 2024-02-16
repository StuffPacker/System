using MediatR;
using SP.Shared.Common.Feature.Event.Dto;

namespace Sp.Api.Business.Feature.Event.GetEventByUser;

public class GetEventByUserCommand : IRequest<List<EventDto>>
{
    public GetEventByUserCommand(Guid currentUserId)
    {
        CurrentUserId = currentUserId;
    }

    public Guid CurrentUserId { get; set; }
}