using Sp.Api.Client.Feature.Client;
using SP.Shared.Common;
using SP.Shared.Common.Feature.Item.Dto;
using SP.Shared.Common.Feature.Item.Mapper;
using SP.Shared.Common.Feature.Item.Model;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Client.Feature.Feature.Item;

public class ApiItemClient : IApiItemClient
{
    private readonly ISpApiClient _apiClient;
    private readonly IItemModelMapper _itemModelMapper;

    public ApiItemClient(ISpApiClient apiClient, IItemModelMapper itemModelMapper)
    {
        _apiClient = apiClient;
        _itemModelMapper = itemModelMapper;
    }

    public async Task<IEnumerable<ItemModel>> GetItemsByUser(Guid userId)
    {
        var result = await _apiClient.GetSecure("SpApi/v1/item/", userId.ToString());
        if (result == null)
        {
            return new List<ItemModel>();
        }

        var dtos = JsonHandler.Deserialize<List<ItemDto>>(result);
        var models = _itemModelMapper.Map(dtos!);
        return models;
    }

    public async Task<ItemModel> Create(Guid userId)
    {
        var dtoIn = string.Empty;
        var result = await _apiClient.PostSecure("SpApi/v1/item/", userId.ToString(), dtoIn);
        var dto = JsonHandler.Deserialize<ItemDto>(result);
        var model = _itemModelMapper.Map(dto);
        return model;
    }

    public async Task Delete(string id, Guid userId)
    {
        await _apiClient.DeleteSecure("SpApi/v1/item/" + id + "/", userId.ToString());
    }

    public async Task<ItemModel?> GetById(string id, Guid userId)
    {
        var result = await _apiClient.GetSecure("SpApi/v1/item/" + id + "/", userId.ToString());
        if (result == null)
        {
            return null;
        }

        var dto = JsonHandler.Deserialize<ItemDto>(result);
        var model = _itemModelMapper.Map(dto);
        return model;
    }

    public async Task<ItemModel> Update(Guid userId, string id, ItemUpdateInputDto dtoIn)
    {
        var result = await _apiClient.PutSecure("SpApi/v1/item/" + id + "/", userId.ToString(), dtoIn);
        var dto = JsonHandler.Deserialize<ItemDto>(result);
        var model = _itemModelMapper.Map(dto);
        return model;
    }
}