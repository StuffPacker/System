using MediatR;
using SP.Database.Mongo.Feature.Event;
using SP.Shared.Common.Feature.Event.Dto;
using SP.Shared.Common.Feature.Event.Mapper;

namespace Sp.Api.Business.Feature.Event.GetEvents;

public class GetEventsCommandHandler : IRequestHandler<GetEventsCommand, List<EventDto>>
{
    private readonly IEventItemRepository _eventItemRepository;
    private readonly IEventMapper _eventModelMapper;

    public GetEventsCommandHandler(IEventMapper eventModelMapper, IEventItemRepository eventItemRepository)
    {
        _eventModelMapper = eventModelMapper;
        _eventItemRepository = eventItemRepository;
    }

    public async Task<List<EventDto>> Handle(GetEventsCommand request, CancellationToken cancellationToken)
    {
        var result = new List<EventDto>();
        var events = await _eventItemRepository.Get();
        foreach (var item in events)
        {
            result.Add(_eventModelMapper.Map(item));
        }

        return result;
    }
}