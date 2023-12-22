using SP.Shared.Common.Feature.Item.Model;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Client.Feature.Feature.Item;

public interface IApiItemClient
{
    Task<IEnumerable<ItemModel>> GetItemsByUser(Guid userId);
}