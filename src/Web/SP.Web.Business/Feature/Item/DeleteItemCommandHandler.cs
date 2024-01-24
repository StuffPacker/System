using MediatR;
using Sp.Api.Client.Feature.Feature.Item;

namespace SP.Web.Business.Feature.Item;

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, string>
{
    private readonly IApiItemClient _itemClient;

    public DeleteItemCommandHandler(IApiItemClient itemClient)
    {
        _itemClient = itemClient;
    }

    public async Task<string> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        await _itemClient.Delete(request.Id, request.UserId);
        return string.Empty;
    }
}