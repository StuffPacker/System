using MediatR;
using SP.Database.Mongo.Feature.Event;
using SP.Shared.Common.Feature.Event.Dto;
using SP.Shared.Common.Feature.Event.Mapper;

namespace Sp.Api.Business.Feature.Event.CreateEvent;

public class CreateEventsCommandHandler : IRequestHandler<CreateEventsCommand, EventDto>
{
    private readonly IEventItemRepository _eventItemRepository;
    private readonly IEventMapper _eventModelMapper;

    public CreateEventsCommandHandler(IEventItemRepository eventItemRepository, IEventMapper eventModelMapper)
    {
        _eventItemRepository = eventItemRepository;
        _eventModelMapper = eventModelMapper;
    }

    public async Task<EventDto> Handle(CreateEventsCommand request, CancellationToken cancellationToken)
    {
        request.Dto.UserId = request.CurrentUser.ToString();
        var model = _eventModelMapper.Map(request.Dto);
        var result = await _eventItemRepository.Create(model);
        return _eventModelMapper.Map(result!);
    }
}