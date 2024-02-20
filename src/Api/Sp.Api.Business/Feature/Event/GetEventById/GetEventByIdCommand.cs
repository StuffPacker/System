using MediatR;
using SP.Shared.Common.Feature.Event.Dto;

namespace Sp.Api.Business.Feature.Event.GetEventById;

public class GetEventByIdCommand : IRequest<EventDto>
{
    public GetEventByIdCommand(string id, Guid currentUser)
    {
        Id = id;
        CurrentUser = currentUser;
    }

    public Guid CurrentUser { get; set; }

    public string Id { get; set; }
}