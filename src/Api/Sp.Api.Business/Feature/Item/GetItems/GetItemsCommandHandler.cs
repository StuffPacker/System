using MediatR;
using SP.Shared.Common.Feature.Database.UserItem;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Business.Feature.Item.GetItems;

public class GetItemsCommandHandler : IRequestHandler<GetItemsCommand, List<ItemDto>>
{
    private readonly IUserItemRepository _userItemRepository;

    public GetItemsCommandHandler(IUserItemRepository userItemRepository)
    {
        _userItemRepository = userItemRepository;
    }

    public async Task<List<ItemDto>> Handle(GetItemsCommand request, CancellationToken cancellationToken)
    {
        return await GetItems(request.UserId);
    }

    private async Task<List<ItemDto>> GetItems(Guid userId)
    {
        var result = new List<ItemDto>();
        var userItems = await _userItemRepository.GetByUserId(userId);
        foreach (var item in userItems)
        {
            // result.Add(_itemModelMapper.Map(item));
        }

        return result.OrderBy(o => o.Name).ToList();
    }
}