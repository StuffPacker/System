using System.Text.Json;
using Sp.Api.Client.Feature.Client;
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
        var dtos = JsonSerializer.Deserialize<List<ItemDto>>(result);
        var models = _itemModelMapper.Map(dtos!);
        return models;
    }
}