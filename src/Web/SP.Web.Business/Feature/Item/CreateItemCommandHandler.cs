using MediatR;
using Sp.Api.Client.Feature.Feature.Item;

namespace SP.Web.Business.Feature.Item;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemViewModel>
{
    private readonly IApiItemClient _itemClient;

    public CreateItemCommandHandler(IApiItemClient itemClient)
    {
        _itemClient = itemClient;
    }

    public async Task<ItemViewModel> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var model = await _itemClient.Create(request.UserId);
        return new ItemViewModel(model);
    }
}