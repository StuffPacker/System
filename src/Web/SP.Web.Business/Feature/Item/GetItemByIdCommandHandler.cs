using MediatR;
using Sp.Api.Client.Feature.Feature.Item;

namespace SP.Web.Business.Feature.Item;

public class GetItemByIdCommandHandler : IRequestHandler<GetItemByIdCommand, ItemViewModel>
{
    private readonly IApiItemClient _itemClient;

    public GetItemByIdCommandHandler(IApiItemClient itemClient)
    {
        _itemClient = itemClient;
    }

    public async Task<ItemViewModel> Handle(GetItemByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await _itemClient.GetById(request.Id, request.UserId);
        return new ItemViewModel(result!);
    }
}