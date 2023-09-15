using MediatR;

namespace SP.Web.Business.Feature.Item.GetItems;

public class GetItemsCommand : IRequest<List<ItemViewModel>>
{
    public GetItemsCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}