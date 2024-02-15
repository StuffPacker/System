using SP.Shared.Common.Feature.Event.Model;

namespace Sp.Api.Client.Feature.Event;

public interface IApiEventClient
{
    Task<List<EventModel>> Get(string currentUser);

    Task<EventModel> Create(Guid requestCurrentUser);
}