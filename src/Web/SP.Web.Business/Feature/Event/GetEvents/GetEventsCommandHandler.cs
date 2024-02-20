using MediatR;
using Sp.Api.Client.Feature.Event;
using SP.Shared.Common.Feature.Event.Model;

namespace SP.Web.Business.Feature.Event.GetEvents;

public class GetEventsCommandHandler : IRequestHandler<GetEventsCommand, List<EventModel>>
{
    private readonly IApiEventClient _apiEventClient;

    public GetEventsCommandHandler(IApiEventClient apiEventClient)
    {
        _apiEventClient = apiEventClient;
    }

    public async Task<List<EventModel>> Handle(GetEventsCommand request, CancellationToken cancellationToken)
    {
        var result = await _apiEventClient.GetByUser(request.UserId);
        return result;
    }
}