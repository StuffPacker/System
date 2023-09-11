using SP.Shared.Common.Feature.Item.Model;

namespace SP.Shared.Common.Feature.Database.UserItem;

public interface IUserItemRepository
{
    Task<ItemModel> Create(ItemModel model);

    Task<ItemModel> GetById(string id);

    Task Update(ItemModel model);

    Task Delete(string id);

    Task<IEnumerable<ItemModel>> GetByUserId(Guid userId);
}