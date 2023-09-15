using MediatR;
using SP.Shared.Common.Feature.Database.UserItem;

namespace SP.Web.Business.Feature.Item.GetItems;

public class GetItemsCommandHandler : IRequestHandler<GetItemsCommand, List<ItemViewModel>>
{
    private readonly IUserItemRepository _userItemRepository;

    public GetItemsCommandHandler(IUserItemRepository userItemRepository)
    {
        _userItemRepository = userItemRepository;
    }

    public async Task<List<ItemViewModel>> Handle(GetItemsCommand request, CancellationToken cancellationToken)
    {
        return await GetItems(request.UserId);
    }

    private async Task<List<ItemViewModel>> GetItems(Guid userId)
    {
        var result = new List<ItemViewModel>();
        var userItems = await _userItemRepository.GetByUserId(userId);
        foreach (var item in userItems)
        {
            result.Add(new ItemViewModel(item));
        }

        return result;
    }
}