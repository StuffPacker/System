using MediatR;

namespace SP.Web.Business.Feature.Item;

public class GetItemByIdCommand : IRequest<ItemViewModel>
{
    public GetItemByIdCommand(string id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    public Guid UserId { get; set; }

    public string Id { get; set; }
}