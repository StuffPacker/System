using MediatR;
using Sp.Api.Client.Feature.Feature.Item;
using SP.Shared.Common.Feature.Item.Mapper;

namespace SP.Web.Business.Feature.Item;

public class GetItemsCommandHandler : IRequestHandler<GetItemsCommand, List<ItemViewModel>>
{
    private readonly IApiItemClient _itemClient;

    public GetItemsCommandHandler(IApiItemClient itemClient, IItemModelMapper itemModelMapper)
    {
        _itemClient = itemClient;
    }

    public async Task<List<ItemViewModel>> Handle(GetItemsCommand request, CancellationToken cancellationToken)
    {
        var models = await _itemClient.GetItemsByUser(request.UserId);

        var list = new List<ItemViewModel>();
        foreach (var item in models)
        {
            list.Add(new ItemViewModel(item));
        }

        return list;
    }
}