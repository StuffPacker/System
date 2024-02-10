using System.Text.Json;
using Sp.Api.Client.Feature.Client;
using SP.Shared.Common;
using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.PackingList.Mapper;
using SP.Shared.Common.Feature.PackingList.Model;

namespace Sp.Api.Client.Feature.PackingList;

public class ApiPackingListClient : IApiPackingListClient
{
    private readonly ISpApiClient _apiClient;
    private readonly IPackingListMapper _packingListMapper;

    public ApiPackingListClient(ISpApiClient apiClient, IPackingListMapper packingListMapper)
    {
        _apiClient = apiClient;
        _packingListMapper = packingListMapper;
    }

    public async Task<PackingListModel?> GetPackingList(string id, Guid currentUser)
    {
        var result = await _apiClient.GetSecure("SpApi/v1/packinglist/" + id, currentUser.ToString());
        if (string.IsNullOrEmpty(result))
        {
            return null;
        }

        var dto = JsonHandler.Deserialize<PackingListDto>(result);
        var model = _packingListMapper.Map(dto!);
        return model;
    }

    public async Task<PackingListModel> GetPackingListPublic(string id)
    {
            var result = await _apiClient.Get("SpApi/v1/packinglist/" + id + "/public");
            var dto = JsonHandler.Deserialize<PackingListDto>(result);
            var model = _packingListMapper.Map(dto!);
            return model;
    }

    public async Task<PackingListModel> GetPackingListPrint(string id, Guid currentUser)
    {
        var result = await _apiClient.GetSecure("SpApi/v1/packinglist/" + id + "/print", currentUser.ToString());
        var dto = JsonHandler.Deserialize<PackingListDto>(result!);
        var model = _packingListMapper.Map(dto!);
        return model;
    }

    public async Task<IEnumerable<PackingListModel>> GetPackingLists(Guid userId)
    {
        var result = await _apiClient.GetSecure("SpApi/v1/packinglist/", userId.ToString());
        if (result == null)
        {
            return new List<PackingListModel>();
        }

        var dtos = JsonHandler.Deserialize<List<PackingListDto>>(result);
        var models = _packingListMapper.Map(dtos!);
        return models;
    }

    public async Task<PackingListModel> Create(Guid userId, PackingListModel model)
    {
        var dtoIn = _packingListMapper.Map(model);
        var result = await _apiClient.PostSecure("SpApi/v1/packinglist/", userId.ToString(), dtoIn);
        var dto = JsonHandler.Deserialize<PackingListDto>(result);
        return _packingListMapper.Map(dto!);
    }

    public async Task Delete(string id, Guid userId)
    {
        await _apiClient.DeleteSecure("SpApi/v1/packinglist/" + id + "/", userId.ToString());
    }

    public async Task Update(PackingListModel model, Guid userId)
    {
        var dto = _packingListMapper.Map(model);
        var result = await _apiClient.PutSecure("SpApi/v1/packinglist/" + model.Id + "/", userId.ToString(), dto);
    }

    public async Task<IEnumerable<PackingListModel>> GetPackingListsPublic()
    {
        var result = await _apiClient.Get("SpApi/v1/packinglist/public");
        var dtos = JsonHandler.Deserialize<List<PackingListDto>>(result);
        var models = _packingListMapper.Map(dtos!);
        return models;
    }

    public async Task<IEnumerable<PackingListModel>> GetPackingListsByUserId(Guid userId, Guid currentUserId)
    {
        var result = await _apiClient.GetSecure("SpApi/v1/user/" + userId.ToString() + "/packinglist/", currentUserId.ToString());

        var dtos = JsonHandler.Deserialize<List<PackingListDto>>(result!);
        var models = _packingListMapper.Map(dtos!);
        return models;
    }
}