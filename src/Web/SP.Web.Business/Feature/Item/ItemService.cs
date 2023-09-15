using System.Security.Authentication;
using SP.Shared.Common.Feature.Database.UserItem;
using SP.Shared.Common.Feature.Item.Model;

namespace SP.Web.Business.Feature.Item;

public class ItemService : IItemService
{
    private readonly IUserItemRepository _userItemRepository;

    public ItemService(IUserItemRepository userItemRepository)
    {
        _userItemRepository = userItemRepository;
    }

    public async Task<List<ItemViewModel>> GetItems(Guid userId)
    {
        var result = new List<ItemViewModel>();
        var userItems = await _userItemRepository.GetByUserId(userId);
        foreach (var item in userItems)
        {
            result.Add(new ItemViewModel(item));
        }

        return result;
    }

    public async Task<ItemViewModel> CreateItem(Guid userId)
    {
        var model = new ItemModel()
        {
            Name = "new item " + DateTime.UtcNow.ToShortDateString(),
            UserId = userId,
            Weight = 0,
            WeightSufix = "g"
        };
        var item = await _userItemRepository.Create(model);
        return new ItemViewModel(item);
    }

    public async Task UpdateItem(string id, Guid userId, ItemEditInputViewModel inputModel)
    {
        var model = await _userItemRepository.GetById(id);
        if (model.UserId != userId)
        {
            throw new AuthenticationException();
        }

        model.Name = inputModel.Name;
        model.ChangeWeight(inputModel.Weight);
        await _userItemRepository.Update(model);
    }

    public async Task Delete(string id, Guid userId)
    {
        var model = await _userItemRepository.GetById(id);
        if (model.UserId != userId)
        {
            throw new AuthenticationException();
        }

        await _userItemRepository.Delete(id);
    }

    public async Task<ItemViewModel> GetItemById(string id, Guid userId)
    {
        var model = await _userItemRepository.GetById(id);
        if (model.UserId != userId)
        {
            throw new AuthenticationException();
        }

        return new ItemViewModel(model);
    }
}