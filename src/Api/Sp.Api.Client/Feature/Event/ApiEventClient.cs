using Sp.Api.Client.Feature.Client;
using SP.Shared.Common;
using SP.Shared.Common.Feature.Event.Dto;
using SP.Shared.Common.Feature.Event.Mapper;
using SP.Shared.Common.Feature.Event.Model;

namespace Sp.Api.Client.Feature.Event;

public class ApiEventClient : IApiEventClient
{
    private readonly ISpApiClient _apiClient;
    private readonly IEventMapper _eventMapper;

    public ApiEventClient(ISpApiClient apiClient, IEventMapper eventMapper)
    {
        _apiClient = apiClient;
        _eventMapper = eventMapper;
    }

    public async Task<List<EventModel>> Get(string currentUser)
    {
        var result = await _apiClient.GetSecure("SpApi/v1/event/", currentUser.ToString());
        if (result == null)
        {
            return new List<EventModel>();
        }

        var dtos = JsonHandler.Deserialize<List<EventDto>>(result);
        var models = _eventMapper.Map(dtos!);
        return models;
    }

    public async Task<List<EventModel>> GetByUser(string currentUser)
    {
        var result = await _apiClient.GetSecure("SpApi/v1/user/" + currentUser + "/event/", currentUser.ToString());
        if (result == null)
        {
            return new List<EventModel>();
        }

        var dtos = JsonHandler.Deserialize<List<EventDto>>(result);
        var models = _eventMapper.Map(dtos!);
        return models;
    }

    public async Task<EventModel> Create(Guid currentUser)
    {
        var dto = new EventDto
        {
            Name = "New Event " + DateTime.UtcNow.Ticks.ToString()
        };
        var result = await _apiClient.PostSecure("SpApi/v1/event/", currentUser.ToString(), dto);
        var newDto = JsonHandler.Deserialize<EventDto>(result);
        return _eventMapper.Map(newDto);
    }

    public async Task<EventModel> GetById(string id, Guid userId)
    {
        var result = await _apiClient.GetSecure("SpApi/v1/event/" + id, userId.ToString());
        if (result == null)
        {
            return null!;
        }

        var dtos = JsonHandler.Deserialize<EventDto>(result);
        var model = _eventMapper.Map(dtos!);
        return model;
    }

    public async Task Delete(string id, Guid userId)
    {
        await _apiClient.DeleteSecure("SpApi/v1/event/" + id, userId.ToString());
    }
}