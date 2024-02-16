using MediatR;
using SP.Database.Mongo.Feature.Event;
using SP.Shared.Common.Feature.Event.Dto;
using SP.Shared.Common.Feature.Event.Mapper;

namespace Sp.Api.Business.Feature.Event.GetEventById;

public class GetEventByIdCommandHandler : IRequestHandler<GetEventByIdCommand, EventDto>
{
    private readonly IEventItemRepository _eventItemRepository;
    private readonly IEventMapper _eventModelMapper;

    public GetEventByIdCommandHandler(IEventItemRepository eventItemRepository, IEventMapper eventModelMapper)
    {
        _eventItemRepository = eventItemRepository;
        _eventModelMapper = eventModelMapper;
    }

    public async Task<EventDto> Handle(GetEventByIdCommand request, CancellationToken cancellationToken)
    {
        var model = await _eventItemRepository.GetById(request.Id);
        return _eventModelMapper.Map(model);
    }
}