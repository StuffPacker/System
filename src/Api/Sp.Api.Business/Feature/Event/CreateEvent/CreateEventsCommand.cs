using MediatR;
using SP.Shared.Common.Feature.Event.Dto;

namespace Sp.Api.Business.Feature.Event.CreateEvent;

public class CreateEventsCommand : IRequest<EventDto>
{
    public CreateEventsCommand(Guid currentUser, EventDto dto)
    {
        CurrentUser = currentUser;
        Dto = dto;
    }

    public EventDto Dto { get; set; }

    public Guid CurrentUser { get; set; }
}