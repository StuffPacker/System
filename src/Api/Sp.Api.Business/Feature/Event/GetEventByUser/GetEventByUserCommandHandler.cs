using MediatR;
using SP.Database.Mongo.Feature.Event;
using SP.Shared.Common.Feature.Event.Dto;
using SP.Shared.Common.Feature.Event.Mapper;

namespace Sp.Api.Business.Feature.Event.GetEventByUser;

public class GetEventByUserCommandHandler : IRequestHandler<GetEventByUserCommand, List<EventDto>>
{
    private readonly IEventItemRepository _eventItemRepository;
    private readonly IEventMapper _eventModelMapper;

    public GetEventByUserCommandHandler(IEventItemRepository eventItemRepository, IEventMapper eventModelMapper)
    {
        _eventItemRepository = eventItemRepository;
        _eventModelMapper = eventModelMapper;
    }

    public async Task<List<EventDto>> Handle(GetEventByUserCommand request, CancellationToken cancellationToken)
    {
        var result = new List<EventDto>();
        var events = await _eventItemRepository.GetByUserId(request.CurrentUserId);
        foreach (var item in events)
        {
            result.Add(_eventModelMapper.Map(item));
        }

        return result;
    }
}