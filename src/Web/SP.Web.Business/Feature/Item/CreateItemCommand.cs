using MediatR;

namespace SP.Web.Business.Feature.Item;

public class CreateItemCommand : IRequest<List<ItemViewModel>>
{
    public CreateItemCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}