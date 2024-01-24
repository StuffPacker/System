using SP.Shared.Common.Feature.Item.Dto;
using SP.Shared.Common.Feature.Item.Model;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Client.Feature.Feature.Item;

public interface IApiItemClient
{
    Task<IEnumerable<ItemModel>> GetItemsByUser(Guid userId);

    Task<ItemModel> Create(Guid userId);

    Task Delete(string id, Guid userId);

    Task<ItemModel> GetById(string requestId, Guid requestUserId);

    Task<ItemModel> Update(Guid userId, string id, ItemEditInputDto model);
}