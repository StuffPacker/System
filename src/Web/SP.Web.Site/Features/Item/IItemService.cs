namespace SP.Web.Site.Features.Item;

public interface IItemService
{
    Task<List<ItemViewModel>> GetItems(Guid userId);

    Task<ItemViewModel> CreateItem(Guid userId);

    Task UpdateItem(string id, Guid userId, ItemEditInputViewModel inputModel);
}