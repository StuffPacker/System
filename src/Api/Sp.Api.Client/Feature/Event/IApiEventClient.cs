using SP.Shared.Common.Feature.Event.Model;

namespace Sp.Api.Client.Feature.Event;

public interface IApiEventClient
{
    Task<List<EventModel>> GetByUser(string currentUser);

    Task<EventModel> Create(Guid requestCurrentUser);

    Task<EventModel> GetById(string id, Guid userId);

    Task Delete(string id, Guid userId);
}