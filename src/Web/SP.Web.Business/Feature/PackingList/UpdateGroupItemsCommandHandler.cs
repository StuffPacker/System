using MediatR;
using SP.Web.Business.Feature.Item;

namespace SP.Web.Business.Feature.PackingList;

public class UpdateGroupItemsCommandHandler : IRequestHandler<UpdateGroupItemsCommand, string>
{
    private readonly IMediator _mediator;
    private readonly IPackingListService _packingListService;

    public UpdateGroupItemsCommandHandler(IPackingListService packingListService, IMediator mediator)
    {
        _packingListService = packingListService;
        _mediator = mediator;
    }

    public async Task<string> Handle(UpdateGroupItemsCommand request, CancellationToken cancellationToken)
    {
        var packingList = await _packingListService.GetPackingListById(request.Id, request.User);
        if (packingList == null)
        {
            throw new Exception();
        }

        foreach (var i in request.SelectedItems)
        {
            var item = await _mediator.Send(new GetItemByIdCommand(i, request.User));
            packingList.AddItemToGroup(request.GroupId, item);
        }

        packingList.RemoveItemsFromGroup(request.GroupId, request.SelectedItems);
        await _packingListService.Update(packingList, request.User);
        return string.Empty;
    }
}