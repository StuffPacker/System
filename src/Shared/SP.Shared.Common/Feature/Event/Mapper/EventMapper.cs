using SP.Shared.Common.Feature.Event.Dto;
using SP.Shared.Common.Feature.Event.Model;

namespace SP.Shared.Common.Feature.Event.Mapper;

public class EventMapper : IEventMapper
{
    public List<EventModel> Map(List<EventDto> dtos)
    {
        var list = new List<EventModel>();
        foreach (var item in dtos)
        {
            list.Add(Map(item));
        }

        return list;
    }

    public EventModel Map(EventDto dto)
    {
        return new()
        {
            Id = dto.Id,
            Name = dto.Name,
            UserId = Guid.Parse(dto.UserId),
            Users = dto.Users.Select(user => Guid.Parse(user)).ToList()
        };
    }

    public List<EventDto> Map(List<EventModel> dtos)
    {
        var list = new List<EventDto>();
        foreach (var item in dtos)
        {
            list.Add(Map(item));
        }

        return list;
    }

    public EventDto Map(EventModel dto)
    {
        return new EventDto
        {
            Id = dto.Id,
            Name = dto.Name,
            UserId = dto.UserId.ToString(),
            Users = dto.Users.Select(item => item.ToString()).ToList()
        };
    }
}