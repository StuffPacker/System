using MediatR;

namespace SP.Web.Business.Feature.Item.CreateItem;

public class CreateItemCommand : IRequest<ItemViewModel>
{
    public CreateItemCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}