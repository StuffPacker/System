using MediatR;
using SP.Shared.Common.Feature.Database.UserItem;
using SP.Shared.Common.Feature.Item.Mapper;
using SP.Shared.Common.Feature.Item.Model;
using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.PackingList.Mapper;

namespace Sp.Api.Business.Feature.Item.CreateItem;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemDto>
{
    private readonly IUserItemRepository _userItemRepository;
    private readonly IItemModelMapper _itemModelMapper;

    public CreateItemCommandHandler(IUserItemRepository userItemRepository, IItemModelMapper itemModelMapper)
    {
        _userItemRepository = userItemRepository;
        _itemModelMapper = itemModelMapper;
    }

    public async Task<ItemDto> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        return await CreateItem(request.UserId);
    }

    private async Task<ItemDto> CreateItem(Guid userId)
    {
        var model = new ItemModel()
        {
            Name = "new item " + DateTime.UtcNow.ToShortDateString(),
            UserId = userId,
            Weight = 0,
            WeightSufix = "g"
        };
        var item = await _userItemRepository.Create(model);
        return _itemModelMapper.Map(item);
    }
}