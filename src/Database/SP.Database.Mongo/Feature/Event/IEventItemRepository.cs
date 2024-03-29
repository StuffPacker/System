using SP.Shared.Common.Feature.Event.Dto;
using SP.Shared.Common.Feature.Event.Model;

namespace SP.Database.Mongo.Feature.Event;

public interface IEventItemRepository
{
    Task<IEnumerable<EventModel>> Get();

    Task<EventModel> Create(EventModel model);

    Task<IEnumerable<EventModel>> GetByUserId(Guid currentUserId);

    Task Delete(string id);

    Task<EventModel> GetById(string id);
}