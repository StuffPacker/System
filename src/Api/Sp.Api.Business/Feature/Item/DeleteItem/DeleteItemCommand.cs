using MediatR;

namespace Sp.Api.Business.Feature.Item.DeleteItem;

public class DeleteItemCommand : IRequest<string>
{
    public DeleteItemCommand(string id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    public Guid UserId { get; set; }

    public string Id { get; set; }
}