using MediatR;
using Sp.Api.Client.Feature.Event;
using SP.Shared.Common.Feature.Event.Model;
using SP.Web.Business.Feature.Event.GetEvents;

namespace SP.Web.Business.Feature.Event.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventModel>
{
    private readonly IApiEventClient _apiEventClient;

    public CreateEventCommandHandler(IApiEventClient apiEventClient)
    {
        _apiEventClient = apiEventClient;
    }

    public async Task<EventModel> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var result = await _apiEventClient.Create(request.CurrentUser);
        return result;
    }
}