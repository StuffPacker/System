using MediatR;
using SP.Shared.Common.Feature.Database.UserItem;
using SP.Shared.Common.Feature.Item.Model;

namespace SP.Web.Business.Feature.Item.CreateItem;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemViewModel>
{
    private readonly IUserItemRepository _userItemRepository;

    public CreateItemCommandHandler(IUserItemRepository userItemRepository)
    {
        _userItemRepository = userItemRepository;
    }

    public async Task<ItemViewModel> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        return await CreateItem(request.UserId);
    }

    private async Task<ItemViewModel> CreateItem(Guid userId)
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
}