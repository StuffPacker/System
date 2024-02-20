using SP.Shared.Common.Feature.Event.Dto;
using SP.Shared.Common.Feature.Event.Model;

namespace SP.Shared.Common.Feature.Event.Mapper;

public interface IEventMapper
{
    List<EventModel> Map(List<EventDto> dtos);

    EventModel Map(EventDto dto);

    List<EventDto> Map(List<EventModel> dtos);

    EventDto Map(EventModel dto);
}